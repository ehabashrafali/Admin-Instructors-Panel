/*  Add_Question Logic */
var id = 1;
var openPopupButton = [];
var QuestionsListObjects = [];
var correctAns;
document.getElementById('examForm').addEventListener('submit', function (event) {
    event.preventDefault();

    var examName = document.getElementById('examName').value; //Exam Name
    var questionType = document.getElementById('questionType').value; //question type
    var question = document.querySelector('.question textarea[name="questionBody"]').value; //question body
    if (question.length !== 0) {
        question = question.charAt(question.length - 1) == "?" ? question : question + " ?";
    }
    var options = document.querySelectorAll('.question input[name="option"]'); //options: multiple values
    var correctOption = document.querySelector('.question input[name="correctOption"]:checked'); //correct answer
    var mark = document.getElementById('Mark').value; //Mark

    if (question.length === 0) {
        alert('Please enter a question body!');
        return;
    }

    if (mark === null) {
        alert('Please enter a grade for your question!');
        return;
    }


    var optionsHtml, isOptionsEmpty;
    if (questionType === 'Paragraph') {

        var questionHtml = `
                        <div class="question" id="${id}" style="position: relative;">
                            <div>
                                <h5 style="display: inline-block;">Question Body: </h5>
                                <input type="textbox" value="${question}" style="border: none; font-size: 20px; color: blue;">
                            </div>
                            <div>
                                <h5 style="display: inline-block;">Answer: </h5>
                                <input type="textbox" value="${paragraphAnswer}" style="border: none; color: green;">
                            </div>

                            <a class="btn btn-primary editBtn" onclick = "modalEditP(this)">Edit</a>
                            <a class="btn btn-danger"  onclick = "modalDeleteP(this)">Delete</a>
                        </div>
                        `;

        //display the created qesution with the correct answer
        document.getElementById('examQuestions').insertAdjacentHTML('beforeend', questionHtml);

        //reset the question body & answer input to empty
        document.querySelector('.question textarea[name="questionBody"]').value = '';
        document.querySelector('.question textarea[name="questionAns"]').value = '';
    }
    else {
        isOptionsEmpty = true;
        optionsHtml = '';

        options.forEach(function (option) {
            if (option.value.trim() !== '') {
                isOptionsEmpty = false;
                optionsHtml += '' + option.value + ',';
            }
        });

        if (isOptionsEmpty || optionsHtml.split(',').length <= 2) {
            alert('Please enter at least two choices!');
            return;
        }

        if (correctOption === null) {
            alert('Please select the correct answer choice!');
            return;
        }

        correctAns = correctOption.parentElement.querySelector('input[name="option"]').value;


        /*appear after question is created*/
        var questionHtml2 = `
                                            <div class="question" id="${id}" style="position: relative;">
                                                <div>
                                                    <h5 style="display: inline-block;">Question Body: </h5>
                                                    <span class="editable-paragraph" style="border: none; font-size: 20px; color: blue; word-break: break-all;">${question}</span>
                                                </div>
                                                <div>
                                                    <h5 style="display: inline-block;">Choices: </h5>
                                                    <span class="editable-paragraph" style="border: none; color: blue; word-break: break-all;">${optionsHtml}</span>
                                                </div>
                                                <div>
                                                    <h5 style="display: inline-block;">Correct Answer: </h5>
                                                    <span class="editable-paragraph"  style="border: none; color: green; word-break: break-all;">${correctAns}</span>
                                                </div>
                                                <div>
                                                    <h5 style="display: inline-block;">Grade: </h5>
                                                    <span class="editable-paragraph"  style="border: none; color: blue; word-break: break-all;">${mark}</span>
                                                </div>

                                                <a class="btn btn-primary" onclick = "modalEditM(this)">Edit</a>
                                                <a class="btn btn-danger"  onclick = "modalDeleteM(this)">Delete</a>
                                            </div>
                `;


        document.getElementById('examQuestions').insertAdjacentHTML('beforeend', questionHtml2);

        //reset the question body input to empty
        document.querySelector('.question textarea[name="questionBody"]').value = '';

        //reset the question Option input to empty
        options.forEach(function (option) {
            option.value = '';
        });

        //reset the radio btn input to unchecked
        correctOption.checked = false;

    }



    var QuestionObject =
    {
        ID: id,
        Type: "MCQ",
        Body: question + " " + optionsHtml,
        Answer: correctAns,
        Mark: mark,
    }

    QuestionsListObjects.push(QuestionObject);


    id++;
});

//--------------------------------------------------------------------------------------------------------------//


                    /* Edit Question Logic */


//Card elements
const cardModal = new bootstrap.Modal(document.getElementById('cardModal'));
const editQestionBody = document.getElementById('editableTextarea');
const editQuestionChoices = document.getElementById('editableInput1');
const editCorrectAns = document.getElementById('editableInput2');
const editMark = document.getElementById('EditMark');
const saveButton = document.getElementById('saveButton');



//Edit MCQ Question
var parentId, question, optionsHtml, correctAns, Mark;
function modalEditM(btn) {
    const clickedButton = this.event.target;

    // Find the parent element of the clicked button
    var parentDiv = clickedButton.parentElement;

    if (parentDiv && parentDiv.classList.contains('question')) {
        // Get the "id" attribute of the parent element (div with class "question")
        parentId = parentDiv.getAttribute('id');
    }

    question = parentDiv.children[0].querySelector('span').innerHTML;
    optionsHtml = parentDiv.children[1].querySelector('span').innerHTML;
    correctAns = parentDiv.children[2].querySelector('span').innerHTML;
    Mark = parentDiv.children[3].querySelector('span').innerHTML;


    // Show the data of the question in the card
    editQestionBody.value = question;
    editQuestionChoices.value = optionsHtml;
    editCorrectAns.value = correctAns;
    editMark.value = Mark;


    cardModal.show();
}


//Delete MCQ Question
function modalDeleteM(btn) {
    const clickedButton = this.event.target;

    var parentDiv = clickedButton.parentElement;

    if (parentDiv && parentDiv.classList.contains('question')) {
        // Get the "id" attribute of the parent element (div with class "question")
        parentId = parentDiv.getAttribute('id');

    }

    document.getElementById(parentId).remove();


    QuestionsListObjects.splice(parentId,1);


    id--;
}





//Edit Paragraph Question
function modalEditP(btn) {
    const cardModal = new bootstrap.Modal(document.getElementById('cardModalP'));
    const editQestionBody = document.getElementById('editableTextareaP');
    const editCorrectAns = document.getElementById('editableInput2P');
    const saveButton = document.getElementById('saveButtonP');

    const clickedButton = this.event.target;

    // Find the parent element of the clicked button
    var parentDiv = clickedButton.parentElement;

    if (parentDiv && parentDiv.classList.contains('question')) {
        // Get the "id" attribute of the parent element (div with class "question")
        var parentId = parentDiv.getAttribute('id');
    }

    const question = parentDiv.children[0].querySelector('input').value;
    const correctAns = parentDiv.children[1].querySelector('input').value;


    console.log(question, correctAns);

    // Show the data of the question in the card
    editQestionBody.value = question;
    editCorrectAns.value = correctAns;

    cardModal.show();
}




// Event listener for the "Save" btn
saveButton.addEventListener('click', function () {

    question = editQestionBody.value;
    /* optionsHtml = editableInput1.value;*/
    optionsHtml = editQuestionChoices.value;
    var SplitedOptions = optionsHtml.split(',');
    var isEqual = false;
    for (let i = 0; i < SplitedOptions.length; i++) {
        if (SplitedOptions[i] === editCorrectAns.value) {
            correctAns = editCorrectAns.value;
            isEqual = true;
            break;
        }
    }

    if (isEqual == false) {
        alert(`"${editableInput2.value}" is not one of your question choices! `);
        return;
    }

    Mark = editMark.value;

    var newquestionHtml = `
                        <div>
                            <h5 style="display: inline-block;">Question Body: </h5>
                            <span class="editable-paragraph" style="border: none; font-size: 20px; color: blue; word-break: break-all;">${question}</span>
                        </div>
                        <div>
                            <h5 style="display: inline-block;">Choices: </h5>
                            <span class="editable-paragraph" style="border: none; color: blue; word-break: break-all;">${optionsHtml}</span>
                        </div>
                        <div>
                            <h5 style="display: inline-block;">Correct Answer: </h5>
                            <span class="editable-paragraph"  style="border: none; color: green; word-break: break-all;">${correctAns}</span>
                        </div>
                    <div>
                        <h5 style="display: inline-block;">Grade: </h5>
                        <span class="editable-paragraph"  style="border: none; color: blue; word-break: break-all;">${Mark}</span>
                    </div>

                        <a class="btn btn-primary" onclick = "modalEditM(this)">Edit</a>
                        <a class="btn btn-danger"  onclick = "modalDeleteM(this)">Delete</a>
                    `;

    document.getElementById(parentId).innerHTML = ''; //clear the old inner html
    document.getElementById(parentId).insertAdjacentHTML('beforeend', newquestionHtml); //add the new html

    // Close the modal
    cardModal.hide();


    //edit the question in the list that will be sent ot the database too
    for (let i = 0; i < QuestionsListObjects.length; i++) {
        if (QuestionsListObjects[i].ID == parentId) {
            QuestionsListObjects[i].Body = question + " " + optionsHtml;
            QuestionsListObjects[i].correctAns = correctAns;
            QuestionsListObjects[i].Mark = Mark;
        }
    }
});






//MCQ Choices//
document.addEventListener("DOMContentLoaded", function () {
    const questionTypeSelect = document.getElementById("questionType");
    const paragraphQuestionAns = document.getElementById("questionAns");
    const PAnsLable = document.getElementById("PAnsLable");
    const choicesContainer = document.getElementById("choices");
    const deleteQuestionButton = document.getElementById("deleteQuestion");

    // Event listener for question type change
    questionTypeSelect.addEventListener("change", function () {
        if (questionTypeSelect.value === "MCQ") {
            choicesContainer.style.display = "block";
            paragraphQuestionAns.style.display = "none";
            PAnsLable.style.display = "none";
        } else {
            choicesContainer.style.display = "none";

            paragraphQuestionAns.style.display = "block";
            PAnsLable.style.display = "block";
        }
    });



    let counter = 0;

    // Event listener for adding a choice
    choicesContainer.addEventListener("click", function (e) {
        if (e.target.classList.contains("add-choice")) {
            const choiceDiv = document.createElement("div");
            choiceDiv.classList.add("form-group", "choice");
            choiceDiv.innerHTML = `
                                                                        <span class="delete-choice">x</span> &nbsp;
                                                                        <span class="add-choice">+</span>
                                                                        <div class="answer">
                                                                            <input type="text" class="form-control" placeholder="Choice" name="option">
                                                                            <input type="radio" name="correctOption" required  id="correctOption">
                                                                            <span class="tooltiptext">Correct Answer</span>
                                                                        </div>
                                                                        `;

            choicesContainer.insertBefore(choiceDiv, e.target.parentElement);
            counter++;
        }
    });


    // Event listener for removing a choice
    choicesContainer.addEventListener("click", function (e) {
        if (e.target.classList.contains("delete-choice") && counter >= 1) {
            e.target.parentElement.remove();
            counter--;
        }
    });
});


// Submit the whole Exam
document.getElementById('submitExam').addEventListener('click', function ()
{
    if (QuestionsListObjects.length === 0) {
        alert("The exam must contain at least one question!");
        return;
    }

    var serializedArray = JSON.stringify(QuestionsListObjects); //the whole array goes to the input as one string, each index(question) in the array is sperated by comma,
    document.getElementById("Questions").value = serializedArray;

    document.getElementById('examForm').submit();
});