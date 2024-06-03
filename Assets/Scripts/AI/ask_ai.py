from llamaapi import LlamaAPI
from langchain_experimental.llms import ChatLlamaAPI
from flask import Flask, request, jsonify
from langchain.chains import create_tagging_chain

app = Flask(__name__)

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

        schema = {
            "properties": {
                "choice": {
                    "type": "string",
                    "description": "Selected choice from the given choices",
                },
                "explanation": {
                    "type": "string",
                    "description": "Reason for choosing the option",
                }
            }
        }

        chain = create_tagging_chain(schema, model)
        response = chain.run(f"This is your conversation history: {message}. Now, make your choice from the given last 3 'given choices' and provide an explanation why you chose this. Do not forget to consider the previous selected choices made in the dialogue history.")
        print(response)

        return jsonify(response)
    except Exception as e:
        return jsonify({"error": str(e)}), 500


if __name__ == "__main__":
    app.run(debug=True)