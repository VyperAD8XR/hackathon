using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using Unity.VisualScripting;

public class Tablet : MonoBehaviour
{
    [HideInInspector]
    public static Tablet Instance;
    public TextMeshProUGUI question;
    public Button answerButtonTemplate;

    private string _correctAnswer;
    private int _intCorrectAnswerIndex;
    private List<Button> _answerButtons;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // LoadQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadAnswers(QuestionsObject _question)
    {
        
        int _answerIndex = 0;
        int _questaionIndex = 0;
        int _totalAnswers;
        List<Button> _availableButtons = new List<Button>();
        Button button;


        //foreach (answerButtons _button in answerButtons){_button.available = true; }
        //_availableButtons = answerButtons.Where(x => x.available).ToList();

    

        foreach (GameObject _answerButton in GameObject.FindGameObjectsWithTag("AnswerButton"))
        {
            GameObject.Destroy(_answerButton);
        }
        answerButtonTemplate.gameObject.SetActive(true);
        for (int i = 0; i < _question.incorrectAnswers.Count + 1; i++)
        {

            _answerButtons.Add(Instantiate(answerButtonTemplate, answerButtonTemplate.transform.parent.transform));
            _answerButtons[i].tag = "AnswerButton";
        }

        _availableButtons = _answerButtons;
        question.text = _question.question;
        _correctAnswer = _question.correctAnswer;

        while (_availableButtons.Count != 0)
        {
            _answerIndex = Random.Range(0, _availableButtons.Count);
            if (_availableButtons.Count == _question.incorrectAnswers.Count + 1)
            {
                _availableButtons[_answerIndex].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = _correctAnswer;
                _intCorrectAnswerIndex = _answerIndex;
            }
            else
            {
                _availableButtons[_answerIndex].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = _question.incorrectAnswers[0];
                _question.incorrectAnswers.RemoveAt(0);
            }

            _availableButtons.RemoveAt(_answerIndex);
            // answerButtons[_answerIndex].available = false;
        }
        GameManager.Instance.quizQuestions.Questions.RemoveAt(_questaionIndex);
        GameManager.Instance.totalQuestions--;
        answerButtonTemplate.gameObject.SetActive(false);

    }

    void ButtonClick(Button _button)
    {
        switch (_button.tag)
        {
            case "AnswerButton":
                GameManager.Instance.UpdateScore( CheckAnswer(_button);
                break;
            case "NextQButton":
                GameManager.Instance.LoadQuestion();
                break;
        }
    }

    bool CheckAnswer(Button _button)
    {
        for (int i = 0; 0 < _answerButtons.Count - 1; i++)
        {
            _answerButtons[i].GetComponent<TextMeshPro>().color = (i == _intCorrectAnswerIndex ? Color.green : Color.red);
        }

        //Returns whether the answer is correct or not 
        return _button.transform.Find("AnswerText").GetComponent<TextMeshProUGUI>().text == _correctAnswer;
    }


}
