using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PasswordScript : MonoBehaviour
{
    public InputField passwordInput;
    public GameObject PanelButtons;
    public string correctPassword;
    public void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            PanelButtons.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(WrongPassword());
            passwordInput.text = "";
        }
    }
    IEnumerator WrongPassword()
    {
        passwordInput.image.color = Color.red;
        yield return new WaitForSeconds(0.02f);
        passwordInput.image.color = Color.white;
    }
}
