using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEditor;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class TestSuite
{
    
    public CoreGameScript CoreScript;
    private GameObject[] CountOfHeads; //used to count "Avatar" items to be assigned to Actors
    private GameObject Textbox; //The object that text is written to
    private int expectedSocialRoles = 1;
    private int expectedInvestigativeRoles = 2;
    private int expectedProtectiveRoles = 0;
    private int expectedKillerRoles = 1;


    public void SetReferences()
    {
        CoreScript = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<CoreGameScript>();
        Textbox = GameObject.FindGameObjectWithTag("Textbox"); //Textbox is setup
        CountOfHeads = GameObject.FindGameObjectsWithTag("Actor"); //CountOfHeads is setup
        CoreScript.debug = true;
    }
    public void RestartGame()
    {
        CoreScript.Initialize();
        CoreScript.Start();
        Textbox.GetComponent<TextboxScript>().DebugClearText();

    }
    public void ProgressGame()
    {
        CoreScript.PhaseDelay = 1;
        CoreScript.Update();
    }
    /*private static void SetGameScene()
    {
        //int GameSceneIndex = 1;
        //SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        //SceneManager.LoadScene("GameScene");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }*/


    /*public void referenceEventSystem()
    {
        GameObject EventSystem = GameObject.FindGameObjectWithTag("Textbox");
    }*/


    /*[Test]
    public void DebugTest()
    {
        SetCoreScript();
        Assert.Equals(1, CoreScript.debug);

    }*/

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
        //SetReferences();
    }
    
    [UnityTest]
    public IEnumerator VerifyTextbox()
    {
        SetReferences();
        Assert.AreEqual(CoreScript.DebugGetTextbox(), Textbox);
        yield return null;
    }

    [UnityTest]
    public IEnumerator VerifyExists()
    {
        Assert.NotNull(CoreScript);
        yield return null;

    }

    [Test]
    public void OnePlusTwoEqualsFour()
    {
        //_ = Assert.Equals("1", "1");//fault located
        Assert.AreEqual(1, 1);
    }

    [UnityTest]
    public IEnumerator SocialIndexCheck()
    {
        SetReferences();
        Assert.AreEqual(expectedSocialRoles, CoreScript.SocialIndex.Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator InvestigativeIndexCheck()
    {
        SetReferences();
        Assert.AreEqual(expectedInvestigativeRoles, CoreScript.InvestigativeIndex.Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ProtectiveIndexCheck()
    {
        SetReferences();
        Assert.AreEqual(expectedProtectiveRoles, CoreScript.ProtectiveIndex.Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator KillerIndexCheck()
    {
        SetReferences();
        Assert.AreEqual(expectedKillerRoles, CoreScript.KillerIndex.Length);
        yield return null;
    }
    [UnityTest]
    public IEnumerator IndexExclusivityCheck()
    {
        SetReferences();
        int[] CivHat = new int[CoreScript.ActorQuantity];
        for (int i = 0; i < CivHat.Length; i++)
        {
            CivHat[i] = i;
        }
        for (int i = 0; i < CoreScript.InvestigativeIndex.Length; i++)
        {
            CivHat = CivHat.Where(w => w != CoreScript.InvestigativeIndex[i]).ToArray();
        }
        for (int i = 0; i < CoreScript.SocialIndex.Length; i++)
        {
            CivHat = CivHat.Where(w => w != CoreScript.SocialIndex[i]).ToArray();
        }
        for (int i = 0; i < CoreScript.ProtectiveIndex.Length; i++)
        {
            CivHat = CivHat.Where(w => w != CoreScript.ProtectiveIndex[i]).ToArray();
        }
        for (int i = 0; i < CoreScript.KillerIndex.Length; i++)
        {
            CivHat = CivHat.Where(w => w != CoreScript.KillerIndex[i]).ToArray();
        }
        int expectedCiv = CoreScript.ActorQuantity - expectedSocialRoles - expectedInvestigativeRoles - expectedProtectiveRoles - expectedKillerRoles;
        Assert.AreEqual(expectedCiv, CivHat.Length);
        //int[] Hat = new int[CoreScript.ActorQuantity];
        //Hat = CoreScript.SetupHat();
        yield return null;
    }
    /*[Test]//[Test, Scene("GameScene")]
    public void BasicGameObjectFound()
    {
        //an even more basic test used for diagnosing SetCoreGameScript();
        //SetGameScene();
        Assert.NotNull(GameObject.FindWithTag("EventSystem"));
    }*/

    [UnityTest]
    public IEnumerator UnityTestTest()
    {
        Assert.NotNull(GameObject.FindWithTag("EventSystem"));
        yield return null;
    }


    [UnityTest]
    public IEnumerator SetupTextboxTest()
    {
        SetReferences();
        //InitializeGame();
        Assert.AreEqual(CoreScript.DebugGetTextbox(), Textbox);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SetupPlayersTest()
    {
        SetReferences();
        //InitializeGame();
        Assert.AreEqual(12, CountOfHeads.Length);
        yield return null;
    }

    /*
    [UnityTest]
    public IEnumerator Test()
    {
        SetReferences();
        yield return null;
    }
    */

    [UnityTest]
    public IEnumerator DuskTest()
    {
        SetReferences();
        CoreScript.DayToNight();
        Assert.IsTrue(CoreScript.NightSky.activeSelf);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DawnTest()
    {
        SetReferences();
        CoreScript.DayToNight();
        CoreScript.NighttoDay();
        Assert.IsTrue(CoreScript.DaySky.activeSelf);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TextEntryTest()
    {
        SetReferences();
        string exampleText = "TextEntryTest";
        CoreScript.TextboxAppend(exampleText);
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("\n" + exampleText));
        Textbox.GetComponent<TextboxScript>().DebugClearText();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SpriteTest()
    {
        SetReferences();
        CoreScript.ChangeSprite(0, CoreScript.MagnifyingGlassIcon);
        Assert.AreEqual(CoreScript.MagnifyingGlassIcon, CoreScript.CountOfHeads[0].GetComponent<Image>().sprite);
        CoreScript.ChangeSprite(0, CoreScript.CompassIcon);
        yield return null;
    }

    [UnityTest]
    public IEnumerator IndecisiveVoteTest()
    {
        SetReferences();
        CoreScript.SetupVoteTalley();
        CoreScript.TalleyVote();
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("\n" + "The vote ends in indecision, nobody accued enough votes to be sentenced to death."));
        Textbox.GetComponent<TextboxScript>().DebugClearText();
        yield return null;
    }

    [UnityTest]
    public IEnumerator DecisiveVoteTest()
    {
        SetReferences();
        int rng = Random.Range(2, CoreScript.ActorQuantity) - 1;
        CoreScript.SetupVoteTalley();
        CoreScript.DebugHysteria(rng);
        for (int i = 1; i < CoreScript.ActorQuantity; i++)
        {
            CoreScript.ActorVote(i);
        }
        CoreScript.TalleyVote();
        Assert.IsFalse(CoreScript.DebugCheckAlive(rng));
        RestartGame();
        yield return null;
    }

    /*
    [UnityTest]
    public IEnumerator PlayerStatementSetupTest()
    {
        SetReferences();
        yield return null;
    }
    */

    [UnityTest]
    public IEnumerator PlayerStatementTest()
    {
        SetReferences();
        int rng = Random.Range(2, CoreScript.ActorQuantity) - 1;
        CoreScript.GamePhase = 200;
        CoreScript.ButtonClicked(rng);
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("You accuse " + CoreScript.DebugGetName(rng) + "."));
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerNoStatementTest()
    {
        SetReferences();
        CoreScript.GamePhase = 200;
        CoreScript.ButtonClicked(0);
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("You abstain from making any statements."));
        RestartGame();
        yield return null;
    }

    /*
    [UnityTest]
    public IEnumerator PlayerVoteSetupTest()
    {
        SetReferences();
        CoreScript.GamePhase = 19;
        ProgressGame();
        yield return null;
    }
    */

    [UnityTest]
    public IEnumerator PlayerVoteTest()
    {
        SetReferences();
        int rng = Random.Range(2, CoreScript.ActorQuantity) - 1;
        CoreScript.GamePhase = 300;
        CoreScript.ButtonClicked(rng);
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("You vote for " + CoreScript.DebugGetName(rng) + "."));
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerNoVoteTest()
    {
        SetReferences();
        CoreScript.GamePhase = 300;
        CoreScript.ButtonClicked(0);
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("You abstain from voting."));
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerNightTest()
    {
        SetReferences();
        CoreScript.GamePhase = 400;
        CoreScript.ButtonClicked(0);
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AutopilotTest()
    {
        SetReferences();
        for (int i = 0; i < 1000; i++)
        {
            ProgressGame();
        }
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator KnownMurdererTest()
    {
        SetReferences();
        int rng = Random.Range(2, CoreScript.ActorQuantity) - 1;
        CoreScript.DebugHysteria(rng);
        CoreScript.GamePhase = 19;
        for (int i = 1; i < CoreScript.ActorQuantity + 3; i++)
        {
            ProgressGame();
        }
        bool debugMania = false;
        if (Textbox.GetComponent<TextboxScript>().DebugViewText().Contains(" of murder!"))
        {
            debugMania = true;
        }
        else if (Textbox.GetComponent<TextboxScript>().DebugViewText().Contains(" over their whereabouts the previous night."))
        {
            debugMania = true;
        }
        else if (Textbox.GetComponent<TextboxScript>().DebugViewText().Contains(" of witchcraft."))
        {
            debugMania = true;
        }
        else if (Textbox.GetComponent<TextboxScript>().DebugViewText().Contains(" for the recent deaths."))
        {
            debugMania = true;
        }
        Assert.IsTrue(debugMania);
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator MurdererTest()
    {
        SetReferences();
        CoreScript.Murder(CoreScript.KillerIndex[0]);
        Assert.IsTrue(CoreScript.Alive.Length < CoreScript.ActorQuantity);
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator DeathTest()
    {
        SetReferences();
        CoreScript.Death(0);
        Assert.IsTrue(Textbox.GetComponent<TextboxScript>().DebugViewText().Contains("You are dead. Whatever the conclusion to this tale, you will not see it..."));
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator RoleErrorHandlingTest()
    {
        SetReferences();
        CoreScript.DebugSetErrorRoles();
        for (int i = 0; i < (CoreScript.ActorQuantity * 100); i++)
        {
            ProgressGame();
        }
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerRoleHandlingTest()
    {
        SetReferences();
        CoreScript.DebugSetPlayerRoles();
        for (int i = 0; i < (CoreScript.ActorQuantity * 5); i++)
        {
            ProgressGame();
        }
        Assert.AreNotEqual(400, CoreScript.GamePhase);
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator ButtonBasicTest()
    {
        SetReferences();
        CoreScript.ButtonsOn();
        CoreScript.ButtonsOff();
        CoreScript.ButtonsOn();
        CoreScript.ButtonsOff();
        
        RestartGame();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BlockTest()
    {
        SetReferences();
        CoreScript.DebugBlockAll();
        RestartGame();
        yield return null;
    }
    /*[Test]
    public void SceneLoaded()
    {
        //EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
        //SceneManager.LoadScene(1);
        //SceneManager.LoadScene("GameScene");
        //SceneManager.SetActiveScene("GameScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }*/
    /*[Test]
    public void SceneLoaded2()
    {
        //EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
        //SceneManager.LoadScene(1);
        //SceneManager.LoadScene("GameScene");
        //SceneManager.SetActiveScene("GameScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }*/

    /*[Test]
    public void BasicDelayTest()
    {
        //an EXTREMELY basic test to verify SetCoreGameScript() works
        SetCoreGameScript();
        Assert.Equals(CoreScript.PhaseDelay, CoreScript.ShortDelay);
    }*/


    /*[Test]
    public void InitializeTest()
    {
        //More complicated test might require CoreGameScript or Textbox to be defined, so their defining it tested seperetly
        SetCoreGameScript();
        SetTextbox();
        Assert.Equals(CoreScript.getTextbox(), Textbox);
    }*/


    /*[Test]
    public void TextboxTest()
    {
        SetCoreGameScript();
        SetTextbox();
        CoreScript.TextboxAppend("Test");
        Assert.Equals(Textbox.GetComponent<TextMeshProUGUI>().text, "Test");
    }*/

    // A Test behaves as an ordinary method
    /*[Test]
    public void TestingTestScriptSimplePasses()
    {
        //Assert.Equals(1, debug);// Use the Assert class to test conditions
    }*/

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /*[UnityTest]
    public IEnumerator TestingTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}
