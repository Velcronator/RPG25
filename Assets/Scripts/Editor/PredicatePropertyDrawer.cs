using System.Collections.Generic;
using System.Linq;
using GameDevTV.Inventories;
using RPG.Quests;
using UnityEditor;
using UnityEngine;

namespace GameDevTV.Utils.Editor
{
    [CustomPropertyDrawer(typeof(Condition.Predicate))]
    public class PredicatePropertyDrawer : PropertyDrawer
    {

        private Dictionary<string, Quest> quests;
        private Dictionary<string, InventoryItem> items;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty predicate = property.FindPropertyRelative("predicate");
            SerializedProperty parameters = property.FindPropertyRelative("parameters");
            SerializedProperty negate = property.FindPropertyRelative("negate");
            float propHeight = EditorGUI.GetPropertyHeight(predicate);
            position.height = propHeight;
            EditorGUI.PropertyField(position, predicate);

            EPredicate selectedPredicate = (EPredicate)predicate.enumValueIndex;

            if (selectedPredicate == EPredicate.Select) return; //Stop drawing if there's no predicate
            while (parameters.arraySize < 2)
            {
                parameters.InsertArrayElementAtIndex(0);
            }
            SerializedProperty parameterZero = parameters.GetArrayElementAtIndex(0);
            SerializedProperty parameterOne = parameters.GetArrayElementAtIndex(1); //Edit: was accidentally 0 in first draft
            if (selectedPredicate == EPredicate.HasQuest || selectedPredicate == EPredicate.CompletedQuest || selectedPredicate == EPredicate.CompletedObjective)
            {
                position.y += propHeight;
                DrawQuest(position, parameterZero);
            }
            position.y += propHeight;
            EditorGUI.PropertyField(position, negate);
        }

        private void DrawQuest(Rect position, SerializedProperty element)
        {
            BuildQuestList();
            var names = quests.Keys.ToList();
            Debug.Log(names.Count());
            int index = names.IndexOf(element.stringValue);

            EditorGUI.BeginProperty(position, new GUIContent("Quest:"), element);
            int newIndex = EditorGUI.Popup(position, "Quest:", index, names.ToArray());
            if (newIndex != index)
            {
                element.stringValue = names[newIndex];
            }

            EditorGUI.EndProperty();
        }

        void BuildQuestList()
        {
            Debug.Log("BuildQuests()");
            if (quests != null) return;
            quests = new Dictionary<string, Quest>();
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                Debug.Log($"Adding Quest {quest.name}");
                quests[quest.name] = quest;
            }
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty predicate = property.FindPropertyRelative("predicate");
            float propHeight = EditorGUI.GetPropertyHeight(predicate);
            EPredicate selectedPredicate = (EPredicate)predicate.enumValueIndex;
            switch (selectedPredicate)
            {
                case EPredicate.Select: //No parameters, we only want the bare enum. 
                    return propHeight;
                case EPredicate.HasLevel:       //All of these take 1 parameter
                case EPredicate.CompletedQuest:
                case EPredicate.HasQuest:
                case EPredicate.HasItem:
                case EPredicate.HasItemEquipped:
                    return propHeight * 3.0f; //Predicate + one parameter + negate
                case EPredicate.CompletedObjective: //All of these take 2 parameters
                case EPredicate.HasItems:
                case EPredicate.MinimumTrait:
                    return propHeight * 4.0f; //Predicate + 2 parameters + negate;
            }
            return propHeight * 2.0f;
        }


    }
}