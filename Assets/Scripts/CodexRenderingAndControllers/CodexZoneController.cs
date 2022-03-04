using UnityEngine;
using UnityEngine.UIElements;
using DTOs;
using SharedScripts;
using UnityEditor;
using System.Collections.Generic;
using System;

public class CodexZoneController
{

    int[] currentTabs = new int[2] { 0, 0 };

    VisualElement[,] tabs = new VisualElement[2, 3];
    VisualElement[] dataZones = new VisualElement[2];
    VisualTreeAsset creatureZoneTemplate;
    VisualTreeAsset abilityZoneTemplate;
    VisualTreeAsset cardZoneTemplate;
    VisualTreeAsset zoneZoneTemplate;
    VisualTreeAsset keywordZoneTemplate;



    private static CodexZoneController singleton;
    private VisualElement root;
    public static CodexZoneController getCodexZoneController(VisualElement givenRoot)
    {
        if (singleton == null) singleton = new CodexZoneController(givenRoot);
        return singleton;
    }

    public static CodexZoneController getExsistingCodexZoneController()
    {
        if (singleton == null) return null; // TODO add error logging
        return singleton;
    }

    private CodexZoneController(VisualElement root)
    {
        dataZones[0] = root.Q<VisualElement>("DataZone1");
        dataZones[1] = root.Q<VisualElement>("DataZone2");

        tabs[0, 0] = dataZones[0].Q<VisualElement>("Tab1");
        tabs[0, 1] = dataZones[0].Q<VisualElement>("Tab2");
        tabs[0, 2] = dataZones[0].Q<VisualElement>("Tab3");
        tabs[1, 0] = dataZones[1].Q<VisualElement>("Tab1");
        tabs[1, 1] = dataZones[1].Q<VisualElement>("Tab2");
        tabs[1, 2] = dataZones[1].Q<VisualElement>("Tab3");

        // for (int i = 0; i < 2; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         root.Q<Button>("DZ" + (i + 1) + "Tab" + (j + 1)).clicked += (() => this.cahngeTab(i, j));
        //     }
        // }
        root.Q<Button>("DZ1Tab1").clicked += (() => this.cahngeTab(0, 0));
        root.Q<Button>("DZ1Tab2").clicked += (() => this.cahngeTab(0, 1));
        root.Q<Button>("DZ1Tab3").clicked += (() => this.cahngeTab(0, 2));
        root.Q<Button>("DZ2Tab1").clicked += (() => this.cahngeTab(1, 0));
        root.Q<Button>("DZ2Tab2").clicked += (() => this.cahngeTab(1, 1));
        root.Q<Button>("DZ2Tab3").clicked += (() => this.cahngeTab(1, 2));


        creatureZoneTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/UI Zone Templates/CreatureZoneTemplate.uxml");
        abilityZoneTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/UI Zone Templates/AbilityZoneTemplate.uxml");
        cardZoneTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/UI Zone Templates/CardZoneTemplate.uxml");
        zoneZoneTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/UI Zone Templates/ZoneZoneTemplate.uxml");
        keywordZoneTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/UI Zone Templates/KeywordZoneTemplate.uxml");

        //TODO add the last of the templates needed.

    }

    public void displayInZone(System.Object item, int dataZoneTier)
    {
        Debug.Log("displayInZone was called");
        // TODO Feature: if item is already open in another tab, just oppen up that tab
        // TODO Feature: Allow ability to "open in next tab" for Zone 2 to have links not override
        if (!SharedValues.validTemplateClasses.Contains(item.GetType()))
        {
            Debug.LogError("CodexZoneController.displayInZone given an invalid type");
            return;
        }
        // TODO
        switch (item.GetType())
        {
            case System.Type type when type == typeof(CardTemplate):
                renderCardTemplate((item as CardTemplate), dataZoneTier);
                break;
            case System.Type type when type == typeof(AbilityTemplate):
                renderAbilityTemplate((item as AbilityTemplate), dataZoneTier);
                break;
            case System.Type type when type == typeof(CreatureTemplate):
                renderCreatureTemplate((item as CreatureTemplate), dataZoneTier);
                break;
            case System.Type type when type == typeof(Region):
                renderRegion((item as Region), dataZoneTier);
                break;
            case System.Type type when type == typeof(Zone):
                renderZone((item as Zone), dataZoneTier);
                break;
            case System.Type type when type == typeof(CreatureType):
                renderCreatureType((item as CreatureType), dataZoneTier);
                break;
            case System.Type type when type == typeof(KeyWord):
                renderKeyWord((item as KeyWord), dataZoneTier);
                break;
            default:
                Debug.LogError("CodexZoneController.displayInZone: Unknown object type given to displayInZone");
                return;
        }
        dataZones[dataZoneTier].Q<Label>("HeadderTitle").text = (item as DTODisplayable).name;
    }


    void renderCardTemplate(CardTemplate item, int dataZoneTier)
    {
        // clear out Visual Element
        VisualElement element = creatureZoneTemplate.CloneTree();
        tabs[dataZoneTier, currentTabs[dataZoneTier]].Clear();
        tabs[dataZoneTier, currentTabs[dataZoneTier]].Add(element);
        element.Q<Label>("Description").text = "test String 143";


    }

    void renderAbilityTemplate(AbilityTemplate item, int dataZoneTier)
    {

    }

    void renderCreatureTemplate(CreatureTemplate item, int dataZoneTier)
    {
        // clear out Visual Element
        VisualElement element = creatureZoneTemplate.CloneTree();
        tabs[dataZoneTier, currentTabs[dataZoneTier]].Clear();
        tabs[dataZoneTier, currentTabs[dataZoneTier]].Add(element);
        element.Q<Label>("Description").text = item.description;

        VisualElement types = element.Q<VisualElement>("Types");
        VisualElement abilities = element.Q<VisualElement>("Abilities");
        VisualElement zones = element.Q<VisualElement>("Zones");
        VisualElement keywords = element.Q<VisualElement>("Keywords");
        VisualElement links = element.Q<VisualElement>("Links");

        // string typeText = "Types: ";
        // foreach (var type in item.types) typeText += type.name;
        // TextElement typeTextElement = new TextElement();
        // typeTextElement.text = typeText;

        types.Add(concatinateToText<CreatureType>(
            "Types:",
            item.types,
            ((CreatureType type) => " " + type.name)
        ));

        abilities.Add(concatinateToText<AbilityTemplate>(
            "Abilities:",
            item.abilities,
            ((AbilityTemplate ability) => " " + ability.name)
        ));

        zones.Add(concatinateToText<Zone>(
            "Zones:",
            item.zones,
            ((Zone zone) => " " + zone.name)
        ));

        keywords.Add(concatinateToText<KeyWord>(
            "Types:",
            item.keyWords,
            ((KeyWord keyword) => " " + keyword.name)
        ));
    }

    void renderRegion(Region item, int dataZoneTier)
    {

    }

    void renderZone(Zone item, int dataZoneTier)
    {

    }

    void renderCreatureType(CreatureType item, int dataZoneTier)
    {

    }

    void renderKeyWord(KeyWord item, int dataZoneTier)
    {

    }


    private TextElement concatinateToText<T>(string startingString, List<T> list, Func<T, string> additionLambda)
    {
        string text = startingString;
        bool first = true;
        foreach (T item in list)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                text += ",";
            }
            text += additionLambda(item);
        }
        TextElement typeTextElement = new TextElement();
        typeTextElement.text = text;
        return typeTextElement;
    }



    public void cahngeTab(int dataZoneTier, int num)
    {
        // TODO
        if (0 > num || num > 2 || 0 > dataZoneTier || dataZoneTier > 1)
        {
            Debug.LogError("cahngeTab called with illegal values: " + dataZoneTier + ", " + num);
            return;
        }
        if (currentTabs[dataZoneTier] != num)
        {
            tabs[dataZoneTier, currentTabs[dataZoneTier]].visible = false;
            tabs[dataZoneTier, num].visible = true;
            currentTabs[dataZoneTier] = num;
        }
    }
}