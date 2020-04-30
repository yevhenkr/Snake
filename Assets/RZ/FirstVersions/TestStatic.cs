using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestStatic
{
    public static StaticValues staticValues = new StaticValues();
}

public class StaticValues
{

    public StaticValues()
    {
        Debug.Log("StaticValues CREATED!");
    }

    public string GetVal()
    {
        return "TestStatic.StaticValues.GetVal()";
    }
}