using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SharedScripts;
using DTOs;
using System;

public class CodexUIController : MonoBehaviour
{

    public VisualElement indexElement;

    private DTODisplayable[] primaryTabs = new DTODisplayable[3];
    private DTODisplayable[] secondaryTabs = new DTODisplayable[3];

    CodexZoneController zoneController;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UnityEngine.UIElements.UIDocument>().rootVisualElement;
        indexElement = root.Q<VisualElement>("indexElement");
        designateButtonActions();
        loadTestEncyclopedia();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void designateButtonActions()
    {
        // RenderCreaturesIndex
        var root = GetComponent<UnityEngine.UIElements.UIDocument>().rootVisualElement;
        zoneController = CodexZoneController.getCodexZoneController(root);
        CodexRendering renderer = CodexRendering.getCodexRendering(root, zoneController);
        root.Q<Button>("RenderCreaturesIndex").clicked += (() => renderer.renderView("Creatures"));
        root.Q<Button>("RenderAbilitiesIndex").clicked += (() => renderer.renderView("Abilities"));
        root.Q<Button>("RenderCardsIndex").clicked += (() => renderer.renderView("Cards"));
        root.Q<Button>("RenderZonesIndex").clicked += (() => renderer.renderView("Zones"));
        root.Q<Button>("RenderKeywordsIndex").clicked += (() => renderer.renderView("Keywords"));

    }
    private void loadTestEncyclopedia()
    {
        Encyclopedia e = CodexScripts.getEncyclopedia() as Encyclopedia;
        CreatureType waterType = new CreatureType(
            "Water",
            "Water",
            new List<string> { "Grass" },
            new List<string> { "Fire" },
            true
            );
        CreatureType grassType = new CreatureType(
            "Grass",
            "Grass",
            new List<string> { "Fire" },
            new List<string> { "Water" },
            true
        );
        CreatureType fireType = new CreatureType(
            "Fire",
            "Fire",
            new List<string> { "Water" },
            new List<string> { "Grass" },
            true
        );
        e.types.Add(waterType);
        e.types.Add(grassType);
        e.types.Add(fireType);

        Zone dFields = new Zone(
            "Dusty Fields",
            ""
        );
        Zone aGrove = new Zone(
            "Autom Grove",
            ""
        );
        Zone sCave = new Zone(
            "Silver Cave",
            ""
        );
        Zone sTown = new Zone(
            "Seashore Town",
            ""
        );
        e.zones.Add(dFields);
        e.zones.Add(aGrove);
        e.zones.Add(sCave);
        e.zones.Add(sTown);

        CreatureTemplate creature1 = new CreatureTemplate("Fire Headgehog", "");
        creature1.description = "Firey adorable little pig with a happit of bruling on everything.";
        creature1.types.Add(fireType);
        creature1.zones.Add(dFields);

        CreatureTemplate creature2 = new CreatureTemplate("Blue Frog", "");
        creature2.description = "This slimy little guy is a very picky eatter.";
        creature2.types.Add(waterType);
        creature2.zones.Add(aGrove);
        creature2.zones.Add(sTown);

        CreatureTemplate creature3 = new CreatureTemplate("Flowering Snail", "");
        creature3.description = "Faster than you might think, this creature can pack a punch.";
        creature3.types.Add(grassType);
        creature3.zones.Add(dFields);
        creature3.zones.Add(aGrove);

        e.creatures.Add(creature1);
        e.creatures.Add(creature2);
        e.creatures.Add(creature3);
    }

}
