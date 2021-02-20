using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Networking
{
	public class Registration : MonoBehaviour
	{
		public TMP_InputField nameField;
		public TMP_InputField passField;

		public Button submit;

		public void CallRegister()
		{
			StartCoroutine(Register());
		}

		private IEnumerator Register()
		{
			WWWForm form = new WWWForm();
			form.AddField("name", nameField.text);
			form.AddField("password", passField.text);
			
			WWW www = new WWW("http://sqlconnect/register.php", form); 
			yield return www;
			if (www.text == "0")
			{
				Debug.Log("User created successfully.");
				SceneManager.LoadScene(0);
			}
			else
			{
				Debug.Log("User creatioin failder. Error #" + www.text);				
			}
		}

		public void VerifyInputs()
		{
			submit.interactable = (nameField.text.Length >= 8 && passField.text.Length >= 8);
		}
	}
}