from llamaapi import LlamaAPI
from langchain_experimental.llms import ChatLlamaAPI
from flask import Flask, request, jsonify
from langchain.chains import create_tagging_chain

app = Flask(__name__)

def extract_choices(message):
    choices = message.split("=> situation :")[-1].split("given choice :")[1:]
    new_choices = []
    for choice in choices:
        choice = choice.replace("(","").replace(")","").replace("=>","")
        new_choices.append(choice)
    return new_choices

@app.route("/api_1", methods=["POST"])
def llm_call():
    try:
        data = request.get_json()
        if not data:
            return jsonify({"error": "No JSON data received"}), 400

        if "message" not in data:
            return jsonify({"error": "Invalid request, 'message' field is required"}), 400

        message = data["message"]
        llama = LlamaAPI('LL-tIiz5yAz3O9FCbO7yIKtC4heZhKKih3pZjrNQPekUlNwdFTHfLUSf6d5cIaZ12yI')
        model = ChatLlamaAPI(client=llama)
        choices = extract_choices(message)
        schema = {
            "properties": {
                "choice": {
                    "type": "string",
                    "description": "Selected choice from the possible_choices",
                },
                "explanation": {
                    "type": "string",
                    "description": "Reason for choosing the option",
                }
            }
        }

        chain = create_tagging_chain(schema, model)

        prompt = f"Your name is Alduin, and you are a video game character. Based on your conversation_history, you must now make a decision from the possible_choices. Always provide a choice and explanation. Ensure your choice aligns with your established character traits. Explain why you made this choice, making sure your explanation is personable and engaging and contains why this choice aligns with your character. Here is your conversation_history: {message}. Here is the possible_choices: {choices}"
        response = chain.run(prompt)
        print(response)
        return jsonify({"choice": response['choice'], "explanation": response['explanation']})
    except Exception as e:
        return llm_call()
if __name__ == "__main__":
    app.run(debug=True)