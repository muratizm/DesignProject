using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ink.Runtime;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using System.Linq;

public class StoryVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables {get; private set;}

    private Story globalVariablesStory; 
    private const string saveVariablesKey = "INK_VARIABLES";
    public StoryVariables(TextAsset globalsTextFile){

        // initialize the story
        globalVariablesStory = new Story(globalsTextFile.text);

        //if we have saved data, load it
        if(PlayerPrefs.HasKey(saveVariablesKey)){
            globalVariablesStory.state.LoadJson(PlayerPrefs.GetString(saveVariablesKey));
        }

        //initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState){
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name,value);
            Debug.Log("initiliazed global story variable: "+ name + " = " + value);
        }
    }

    public void SaveVariables(){
        if(globalVariablesStory != null){
            VariableToStory(globalVariablesStory);
            PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    }

    public void StartListening(Story story){
        VariableToStory(story); // this has to be before assigning listener
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    
    public void StopListening(Story story){
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    
    private void VariableChanged(string name, Ink.Runtime.Object value){
        if(variables.ContainsKey(name)){
            variables.Remove(name);
            variables.Add(name,value);
        }
    }

    private void VariableToStory(Story story){
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables){
            story. variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
