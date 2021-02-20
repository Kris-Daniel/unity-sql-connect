using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Networking
{
	public class Login : MonoBehaviour
	{
		public TMP_InputField nameField;
		public TMP_InputField passField;

		public Button submit;

		public void CallLogin()
		{
			StartCoroutine(LoginPlayer());
		}

		private IEnumerator LoginPlayer()
		{
			WWWForm form = new WWWForm();
			form.AddField("name", nameField.text);
			form.AddField("password", passField.text);
			
			WWW www = new WWW("http://sqlconnect/login.php", form); 
			yield return www;
			if (www.text[0] == '0')
			{
				DBManager.UserName = nameField.text;
				DBManager.Score = int.Parse(www.text.Split('\t')[1]);
				SceneManager.LoadScene(0);
			}
			else
			{
				Debug.Log("User Login failed, Error #" + www.text);
			}
		}
		
		public void VerifyInputs()
		{
			submit.interactable = (nameField.text.Length >= 8 && passField.text.Length >= 8);
		}
	}
}