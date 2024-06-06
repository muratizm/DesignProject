#pip install langchain_experimental
from llamaapi import LlamaAPI
from langchain_experimental.llms import ChatLlamaAPI
from langchain_core.pydantic_v1 import BaseModel, Field
from langchain_core.output_parsers import JsonOutputParser
from langchain.prompts import PromptTemplate
from flask import Flask, request, jsonify

app = Flask(__name__)


def preprocess_text(message):
    lines = [line.strip() for line in message.strip().split('\n') if line.strip()]

    thought_bubbles = []
    situations = []
    given_choices = []
    previous_choice = None

    for line in lines:
        if "thought bubble" in line:
            thought_bubbles.append(line.split(":", 1)[1].strip())
        elif "situation" in line:
            situations.append(line.split(":", 1)[1].strip())
        elif "given choice" in line:
            given_choices.append(line.split(":", 1)[1].strip())
        elif "choice made" in line:
            previous_choice = line.split(":", 1)[1].strip()

    output_lines = [
        "Your name is Alduin, and you are a video game character. Based on your past choices and characteristics, "
        "you must now make a decision from the given options. Ensure your choice aligns with your established "
        "character traits. You must state your choice and provide an explanation why you chose it.",
        ""
    ]

    if thought_bubbles:
        output_lines.append(f"Thought bubble: {thought_bubbles[0]}")
        output_lines.append("")
    if situations:
        output_lines.append(f"Situation: {situations[0]}")
        output_lines.append("")

    if given_choices:
        output_lines.append("Given choices:")
        for i, choice in enumerate(given_choices[:3], start=1):
            output_lines.append(f"{i}. {choice}")
        output_lines.append("")

    if previous_choice:
        output_lines.append(f"Previous choice: {previous_choice}")
        output_lines.append("")

    if len(thought_bubbles) > 1:
        for bubble in thought_bubbles[1:]:
            output_lines.append(f"Thought bubble: {bubble}")
        output_lines.append("")

    if len(situations) > 1:
        output_lines.append(f"Situation: {situations[1]}")
        output_lines.append("")

    if len(given_choices) > 3:
        output_lines.append("Given choices:")
        for i, choice in enumerate(given_choices[3:], start=1):
            output_lines.append(f"{i}. {choice}")
        output_lines.append("")

    history = "\n".join(output_lines).replace("\"", "")
    return history


class Decision(BaseModel):
    choice: str = Field(description="Selected choice from the possible_choices")
    explanation: str = Field(description="Reason for choosing the option")


def extract_choices(message):
    choices = message.split("=> situation :")[-1].split("given choice :")[1:]
    new_choices = []
    for choice in choices:
        choice = choice.replace("(", "").replace(")", "").replace("=>", "")
        new_choices.append(choice)
    return new_choices


def convert_prompt(instructions):
    B_INST, E_INST = "[INST]", "[/INST]"
    B_SYS, E_SYS = "<<SYS>>\n", "\n<</SYS>>\n\n"

    prompt_temp = B_INST + "Give json as answer." + B_SYS + instructions + E_SYS + E_INST
    return prompt_temp


@app.route("/api_1", methods=["POST"])
def llm_call():
    try:
        data = request.get_json()
        if not data:
            return jsonify({"error": "No JSON data received"}), 400

        if "message" not in data:
            return jsonify({"error": "Invalid request, 'message' field is required"}), 400

        message = preprocess_text(data["message"])
        llama = LlamaAPI('LL-tIiz5yAz3O9FCbO7yIKtC4heZhKKih3pZjrNQPekUlNwdFTHfLUSf6d5cIaZ12yI')
        model = ChatLlamaAPI(client=llama, model='llama3-8b')
        choices = extract_choices(message)
        parser = JsonOutputParser(pydantic_object=Decision)
        prompt = PromptTemplate(
            template=convert_prompt(
                "Your name is Alduin, and you are a person video game character. Based on choices you made in the conversation_history, you must now decide one of the possible_choices provided. Ensure your choice and explanation accurately reflect your character traits demonstrated in the past without any assumptions about being a hero or a good person. You must respect to your character do not be wiiling to change it. Always explain your choice, ensuring it reflects your character traits demonstrated in the past. Your explanation should be engaging, reflecting your personality. Do not share your previous choices explicitly. Here is your conversation_history: {message} and possible_choices: {choices} \n{format_instructions}"),
            input_variables=["message", "choices"],
            partial_variables={"format_instructions": parser.get_format_instructions()},
        )
        chain = prompt | model | parser
        response = chain.invoke({"message": message, "choices": choices})
        return jsonify({"choice": response['choice'], "explanation": response['explanation']})
    except Exception as e:
        print("error", str(e))
        return llm_call()


if __name__ == "__main__":
    app.run(debug=True)
