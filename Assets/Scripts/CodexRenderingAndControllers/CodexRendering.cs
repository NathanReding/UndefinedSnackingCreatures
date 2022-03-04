using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SharedScripts;
using DTOs;
using System;

public class CodexRendering
{
    private string currentView = "";
    private static CodexRendering singleton;
    private CodexZoneController codexZoneController;

    private VisualElement root;
    public static CodexRendering getCodexRendering(VisualElement givenRoot, CodexZoneController codexZoneController)
    {
        if (singleton == null) singleton = new CodexRendering(givenRoot, codexZoneController);
        return singleton;
    }
    public CodexRendering(VisualElement givenRoot, CodexZoneController codexZoneController)
    {
        root = givenRoot;
        Debug.Log("got to CodexRendering constructor. is null?: " + (codexZoneController == null));
        codexZoneController = this.codexZoneController;
    }

    public void renderView(string view)
    {
        if (!SharedValues.validCodexViews.Contains(view))
        {
            Debug.LogError("renderView called with invalid string 'view'.");
            return;
        }
        if (view == currentView)
        {
            Debug.Log("View " + view + " is already loaded.");
            return;
        }
        VisualElement parent = root.Q<VisualElement>("indexElement");
        parent.Clear();

        switch (view)
        {
            case "Creatures":
                renderCreatures(parent);
                break;
            case "Abilities":
                renderAbilities(parent);
                break;
            case "Cards":
                renderCards(parent);
                break;
            case "Zones":
                renderZones(parent);
                break;
            case "Keywords":
                renderKeywords(parent);
                break;
        }
        currentView = view;
    }

    void renderCreatures(VisualElement parent)
    {
        Debug.Log("number of types: " + (CodexScripts.getEncyclopedia() as Encyclopedia).types.Count +
                  "number of creatures" + (CodexScripts.getEncyclopedia() as Encyclopedia).creatures.Count
                  );
        // By Type
        renderDirectoryByX<CreatureType, CreatureTemplate>(
            "By type",
            (CodexScripts.getEncyclopedia() as Encyclopedia).types,
            (CodexScripts.getEncyclopedia() as Encyclopedia).creatures,
            ((CreatureTemplate creature) => creature.types),
            parent
        );
        // By Zone
        renderDirectoryByX<Zone, CreatureTemplate>(
            "By zone",
            (CodexScripts.getEncyclopedia() as Encyclopedia).zones,
            (CodexScripts.getEncyclopedia() as Encyclopedia).creatures,
            ((CreatureTemplate creature) => creature.zones),
            parent
        );
        // Alphabetical
        renderListByX<CreatureTemplate>(
            "Alphabeticaly",
            (CodexScripts.getEncyclopedia() as Encyclopedia).creatures,
            parent
        );
        // Create new 
        renderAddNewButton<CreatureTemplate>(parent);
    }
    void renderAbilities(VisualElement parent)
    {
        // By Type
        renderDirectoryByX<CreatureType, AbilityTemplate>(
            "By type",
            (CodexScripts.getEncyclopedia() as Encyclopedia).types,
            (CodexScripts.getEncyclopedia() as Encyclopedia).abilities,
            ((AbilityTemplate ability) => ability.types),
            parent
        );
        // Alphabetical
        renderListByX<AbilityTemplate>(
            "Alphabeticaly",
            (CodexScripts.getEncyclopedia() as Encyclopedia).abilities,
            parent
        );
        // Create new 
        renderAddNewButton<AbilityTemplate>(parent);
    }
    void renderCards(VisualElement parent)
    {
        // By Type
        renderDirectoryByX<CreatureType, CardTemplate>(
            "By type",
            (CodexScripts.getEncyclopedia() as Encyclopedia).types,
            (CodexScripts.getEncyclopedia() as Encyclopedia).cards,
            ((CardTemplate card) => card.types),
            parent
        );
        // By Zone
        renderDirectoryByX<Zone, CardTemplate>(
            "By zone",
            (CodexScripts.getEncyclopedia() as Encyclopedia).zones,
            (CodexScripts.getEncyclopedia() as Encyclopedia).cards,
            ((CardTemplate card) => card.zones),
            parent
        );
        // Alphabetical
        renderListByX<CardTemplate>(
            "Alphabeticaly",
            (CodexScripts.getEncyclopedia() as Encyclopedia).cards,
            parent
        );
        // Create new 
        renderAddNewButton<CardTemplate>(parent);
    }
    void renderZones(VisualElement parent)
    {
        // Alphabetical
        renderListByX<Zone>(
            "Alphabeticaly",
            (CodexScripts.getEncyclopedia() as Encyclopedia).zones,
            parent
        );
        // Create new 
        renderAddNewButton<Zone>(parent);
    }
    void renderKeywords(VisualElement parent)
    {
        // Alphabetical
        renderListByX<KeyWord>(
            "Alphabeticaly",
            (CodexScripts.getEncyclopedia() as Encyclopedia).keyWords,
            parent
        );
        // Create new 
        renderAddNewButton<KeyWord>(parent);
    }


    // Example
    /*
        Dictionary<string, List<CreatureTemplate>> indexedListType = new Dictionary<string, List<CreatureTemplate>>();
        foreach (CreatureType type in (CodexScripts.getEncyclopedia() as Encyclopedia).types)
        {
            List<CreatureTemplate> temp = new List<CreatureTemplate>();
            indexedListType.Add(type.name, temp);
        }
        foreach (CreatureTemplate creature in (CodexScripts.getEncyclopedia() as Encyclopedia).creatures)
        {
            foreach (CreatureType type in creature.types)
            {
                indexedListType[type.name].Add(creature);
            }
        }
    */
    void renderDirectoryByX<T, Y>(
            string title,
            List<T> groupOfSubdevisers,
            List<Y> groupToBeRendered,
            Func<Y, List<T>> getYSubdidingValue,
            VisualElement parent)
        where T : DTODisplayable
        where Y : DTODisplayable
    {
        // Create the dictionary to be rendered
        Dictionary<string, List<Y>> indexedListType = new Dictionary<string, List<Y>>();
        foreach (T type in groupOfSubdevisers)
        {
            List<Y> temp = new List<Y>();
            indexedListType.Add(type.name, temp);
        }
        Debug.Log("renderDirectoryByX before fore each");
        foreach (Y itemToBeDisplayed in groupToBeRendered)
        {
            Debug.Log("renderDirectoryByX foreach 111");
            foreach (T type in getYSubdidingValue(itemToBeDisplayed))
            {
                Debug.Log("renderDirectoryByX foreach 222");
                indexedListType[type.name].Add(itemToBeDisplayed);
            }
        }
        renderDictionary<Y>(title, indexedListType, parent);
    }

    void renderListByX<T>( // Takes a list and creates a dictionary with key generated by function. Use for alphabetical
            string title,
            List<T> groupToBeRendered,
            VisualElement parentVE)
        where T : DTODisplayable
    {

        List<string> names = new List<string>();
        foreach (T item in groupToBeRendered)
        {
            names.Add(item.name);
        }
        names.Sort();

        Dictionary<string, T> subGroupdictionary = new Dictionary<string, T>();
        foreach (T item in groupToBeRendered)
        {
            subGroupdictionary.Add(item.name, item);
        }
        renderList<T>(title, subGroupdictionary, parentVE);
    }

    void renderDictionary<T>(string name, Dictionary<string, List<T>> dictionary, VisualElement parentVE) where T : DTODisplayable
    {
        List<string> groupNames = new List<string>();
        foreach (string groupName in dictionary.Keys)
        {
            groupNames.Add(groupName);
        }
        groupNames.Sort();

        //create new Foldout
        Foldout foldout = addNewFoldout(name, parentVE);
        foreach (string groupName in groupNames)
        {
            Dictionary<string, T> subGroupdictionary = new Dictionary<string, T>();
            foreach (T item in dictionary[groupName])
            {
                subGroupdictionary.Add(item.name, item);
            }
            renderList<T>(groupName, subGroupdictionary, foldout);
        }
    }

    void renderList<T>(string name, Dictionary<string, T> dictionary, VisualElement parentVE) where T : DTODisplayable
    {
        List<string> itemNames = new List<string>();

        foreach (string itemName in dictionary.Keys)
        {
            itemNames.Add(itemName);
        }
        itemNames.Sort();

        //create new Foldout
        Foldout foldout = addNewFoldout(name, parentVE);
        foreach (string item in itemNames)
        {
            addAButton<T>(item, dictionary[item], foldout);
        }
    }


    Foldout addNewFoldout(string name, VisualElement parentVE)
    {
        // TODO
        Foldout result = new Foldout();
        // set parent
        parentVE.Add(result);
        // give it formatting
        // set it's text
        result.text = name;
        // Close up the foldout
        result.value = false;

        return result;
    }

    void addAButton<T>(string name, T item, VisualElement parentVE) where T : DTODisplayable
    {
        // TODO
        CodexZoneController locallyStoredController = codexZoneController;
        Button result = new Button();
        // System.Action temp = new System.Action(clearIndexView);
        // System.Action temp = new System.Action(() => clearIndexView());
        result.clicked += new System.Action(() =>
        {
            // Debug.Log("item null: " + (item == null) + "   zone controller null:" + (CodexZoneController.getExsistingCodexZoneController() == null));
            CodexZoneController.getExsistingCodexZoneController().displayInZone(item, 0);
        });
        // set parent
        parentVE.Add(result);
        // give it formatting
        // set it's text
        result.text = name;
        // save it's type // typeof(T)
    }

    void renderAddNewButton<T>(VisualElement parentVE)
    {
        // TODO
        Button result = new Button();
        // set parent
        parentVE.Add(result);
        // give it formatting

        // set it's text
        result.text = "+ Add new";
        // give it the function to call

        // save it's type // typeof(T)
    }

    void clickToRenderSecondary<T>(T item) where T : DTODisplayable
    {
        // TODO
        if (!SharedValues.validTemplateClasses.Contains(item.GetType())) return;
        // Switch case to render each of the types it could be given
    }

}