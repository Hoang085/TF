using UnityEditor;
using UnityEditor.SceneManagement;
using H2910.Defines;

public static class SwitchSceneEditor
{
    // [MenuItem("Scene/Splash Scene")]
    // static void SplashScene()
    // {
    //     EditorSceneManager.OpenScene("Assets/Scenes/" + GameDefine.SplashScene + ".unity");
    // }

 
    [MenuItem("-SWITCH SCENE-/Test")]
    static void TestScene()
    {
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/Test.unity");
    }
    
    [MenuItem("-SWITCH SCENE-/Home")]
    static void HomeScene()
    {
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/Home.unity");
    }

    [MenuItem("-SWITCH SCENE-/GamepLay")]
    static void GameplayScene()
    {
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/GamePlay.unity");
    }
    
    [MenuItem("-SWITCH SCENE-/Login")]
    static void LoginScene()
    {
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/Login.unity");
    }
    
    [MenuItem("-SWITCH SCENE-/TestMev")]
    static void TestMevScene()
    { 
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/TestGameplay.unity");
    }
    
    [MenuItem("-SWITCH SCENE-/Test UI")]
    static void TestUIScene()
    {
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/TestUI.unity");
    }
    
    [MenuItem("-SWITCH SCENE-/Test 3")]
    static void Test3Scene()
    {
        EditorSceneManager.OpenScene("Assets/Resources/Scenes/Test3.unity");
    }
    
}
