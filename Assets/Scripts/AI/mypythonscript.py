import os

def say_hello():
    print("Hello from Python!")


def test(message):
    print("Message from Unity: ", message)
    directory = os.getcwd()
    return message + " : " + directory