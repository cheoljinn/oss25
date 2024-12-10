using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    public Text dayText, walletText, evolutionStageText, actionLimitText;
    public Slider hungerBar, affectionBar, cleanBar, intelligenceBar, wilpowerBar, sociaBar;
    public Image evolutionSprite;
    public GameObject polygonStatsDisplay;

    public void UpdateStats(int hunger, int affection, int clean, int intelligence, int willpower, int social)
    {
        hungerBar.value = hunger;
        affectionBar.value = affection;
        cleanBar.value = clean;
        intelligenceBar.value = intelligence;
        wilpowerBar.value = willpower;
        sociaBar.value = social;
    }

    public void UpdateActionLimit(int actionsLeft)
    {
        actionLimitText.text = $"���� Ƚ��: {actionsLeft}"; //�Ϸ� Ȱ�� Ƚ�� ����, Ƚ�� ǥ��
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
