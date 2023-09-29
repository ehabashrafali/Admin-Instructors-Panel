
//// Initialize CodeMirror
//var editor = CodeMirror.fromTextArea(document.getElementById("questionBody"), {
//    mode: "html", // Set the code language mode
//    lineNumbers: true,   // Enable line numbers
//    theme: "dracula"    // Choose a code editor theme
//});



/*  Add_Question Logic */
var id = 1;
var openPopupButton = [];
document.getElementById('examForm').addEventListener('submit', function (event) {
    event.preventDefault();

    var examName = document.getElementById('examName').value; //Exam Name
    var questionType = document.getElementById('questionType').value; //question type
    var question = document.querySelector('.question textarea[name="questionBody"]').value; //question body
    if (question.length !== 0) {
        question = question.charAt(question.length - 1) == "?" ? question : question + " ?";
    }
    var paragraphAnswer = document.querySelector('.question textarea[name="questionAns"]').value; //paragraph answer
    var options = document.querySelectorAll('.question input[name="option"]'); //options: multiple values
    var correctOption = document.querySelector('.question input[name="correctOption"]:checked'); //correct answer

    if (question.length === 0) {
        alert('Please enter a question body');
        return;
    }


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
        var isOptionsEmpty = true;
        var optionsHtml = '';

        options.forEach(function (option) {
            if (option.value.trim() !== '') {
                isOptionsEmpty = false;
                optionsHtml += '' + option.value + ', ';
            }
        });

        if (isOptionsEmpty) {
            alert('Please enter at least one answer choice.');
            return;
        }

        if (correctOption === null) {
            alert('Please select the correct answer choice.');
            return;
        }

        var correctAns = correctOption.parentElement.querySelector('input[name="option"]').value;


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

        //enable submit exam btn
        document.getElementById('submitExam').disabled = false;
    }

    id++;

});

//--------------------------------------------------------------------------------------------------------------//


/* Edit_Question Logic */

const cardModal = new bootstrap.Modal(document.getElementById('cardModal'));
const editQestionBody = document.getElementById('editableTextarea');
const editCorrectAns = document.getElementById('editableInput2');
const saveButton = document.getElementById('saveButton');



var parentId, question, optionsHtml, correctAns;
function modalEditM(btn) //Edit MCQ Question
{
    // Get references to the modal and elements

    const editQuestionChoices = document.getElementById('editableInput1');


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


    // Show the data of the question in the card
    editQestionBody.value = question;
    editQuestionChoices.value = optionsHtml;
    editCorrectAns.value = correctAns;

    cardModal.show();
}


function modalDeleteM(btn) {
    const clickedButton = this.event.target;

    var parentDiv = clickedButton.parentElement;

    if (parentDiv && parentDiv.classList.contains('question')) {
        // Get the "id" attribute of the parent element (div with class "question")
        parentId = parentDiv.getAttribute('id');

    }

    document.getElementById(parentId).remove();
}





function modalEditP(btn) //Edit Paragraph Question
{
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
    //save the new values to the initial values
    var newquestion = editQestionBody.value;
    var newoptionsHtml = editableInput1.value;
    var newcorrectAns = editableInput2.value;

    var newquestionHtml = `
                                    <div>
                                        <h5 style="display: inline-block;">Question Body: </h5>
                                        <span class="editable-paragraph" style="border: none; font-size: 20px; color: blue; word-break: break-all;">${newquestion}</span>
                                    </div>
                                    <div>
                                        <h5 style="display: inline-block;">Choices: </h5>
                                        <span class="editable-paragraph" style="border: none; color: blue; word-break: break-all;">${newoptionsHtml}</span>
                                    </div>
                                    <div>
                                        <h5 style="display: inline-block;">Correct Answer: </h5>
                                        <span class="editable-paragraph"  style="border: none; color: green; word-break: break-all;">${newcorrectAns}</span>
                                    </div>

                                    <a class="btn btn-primary" onclick = "modalEditM(this)">Edit</a>
                                    <a class="btn btn-danger"  onclick = "modalDeleteM(this)">Delete</a>
        `;

    document.getElementById(parentId).innerHTML = ''; //clear the old inner html
    document.getElementById(parentId).insertAdjacentHTML('beforeend', newquestionHtml); //add the new html

    // Close the modal
    cardModal.hide();
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




document.getElementById('submitExam').addEventListener('click', function () {
    var examQuestions = document.getElementById('examQuestions').innerHTML;
    downloadExam(examQuestions);
});

function downloadExam(examQuestions) {
    var dataStr = "data:text/plain;charset=utf-8," + encodeURIComponent(examQuestions);
    var downloadAnchor = document.createElement('a');
    downloadAnchor.setAttribute("href", dataStr);
    downloadAnchor.setAttribute("download", "exam.txt");
    downloadAnchor.style.display = "none";
    document.body.appendChild(downloadAnchor);
    downloadAnchor.click();
    document.body.removeChild(downloadAnchor);
}