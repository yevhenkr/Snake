using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email : MonoBehaviour
{
     public void SendEmail()
  {
    string email = "tarasawrik@gmail.com";
    string subject = MyEscapeURL("My Subject");
    string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
    Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);

    string MyEscapeURL(string url)
    {
      return WWW.EscapeURL(url).Replace("+", "%20");
    }

  }
}
