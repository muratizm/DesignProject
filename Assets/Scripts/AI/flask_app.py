from flask import Flask, request, jsonify
from llamaapi import LlamaAPI

app = Flask(__name__)

@app.route("/api_1", methods=["POST"])
def llm_call():
    data = request.get_json()  
    message = data.get("message")
    llama = LlamaAPI('LL-tIiz5yAz3O9FCbO7yIKtC4heZhKKih3pZjrNQPekUlNwdFTHfLUSf6d5cIaZ12yI')
    prompt = "Identify the personality type based on the given message. Your prediction must match one of these personality types: ['Brave', 'Friendly', 'Helpful', 'Logical', 'Warrior'] Respond with only the matched personality type in the specified format ('personality type') without saying anything else like emojis. You have to return only the personality type as output. Message: {message}"
    api_request_json = {
        "model": "llama-13b-chat",
        "messages": [
            {"role": "system", "content": "You are a classifier."},
            {"role": "user", "content": prompt.format(message=message)},
        ]
    }
    response = llama.run(api_request_json)

    if response.ok:
        json_response = response.json()
        content = json_response.get("choices", [{}])[0].get("message", {}).get("content", "")
        return jsonify({"personality_type": content})

    return jsonify({"error": "Failed to get response from LlamaAPI"}), 500

if __name__ == "__main__":
    app.run(debug=True)
