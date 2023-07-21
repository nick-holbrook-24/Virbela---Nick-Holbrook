using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace ExerciseOne
{
    public class XMLReader : MonoBehaviour
    {
        public List<List<string>> GetXMLTextLists(string _targetXMLFilename)
        {
            TextAsset xmlAsset = Resources.Load<TextAsset>(_targetXMLFilename);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlAsset.text);

            XmlNodeList itemNodes = xmlDoc.SelectNodes("/ItemCollection/Items/Item");

            List<string> itemNames = new List<string>();
            List<string> itemDescriptions = new List<string>();

            foreach (XmlNode itemNode in itemNodes)
            {
                string itemName = itemNode.Attributes["name"].Value;
                string itemDescription = itemNode.SelectSingleNode("Description").InnerText;

                itemNames.Add(itemName);
                itemDescriptions.Add(itemDescription);
            }

            List<List<string>> xmlLists = new List<List<string>>
            {
                itemNames,
                itemDescriptions
            };

            return xmlLists;
        }
    }
}