using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private SerializableDictionary<int, string> test;

		public TextMeshProUGUI playerDisplay;

		private void Start()
		{
			if (DBManager.LoggedIn)
			{
				playerDisplay.text = "Player: " + DBManager.UserName;
			}
		}

		public void GoToRegister()
		{
			SceneManager.LoadScene(1);
		}
		
		public void GoToLogin()
		{
			SceneManager.LoadScene(2);
		}
	}
}