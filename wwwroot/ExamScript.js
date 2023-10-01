
//// Initialize CodeMirror
//var editor = CodeMirror.fromTextArea(document.getElementById("questionBody"), {
//    mode: "html", // Set the code language mode
//    lineNumbers: true,   // Enable line numbers
//    theme: "dracula"    // Choose a code editor theme
//});

function Question(Type, Body, Answer, Mark) {
    this.Type = Type;
    this.Body = Body;
    this.Answer = Answer;
    this.Mark = Mark;
}

var global_questions = [];


function Exam(Name, Duration, CreationDate, Questions) {
    this.Name = Name;
    this.Duration = Duration;
    this.CreationDate = CreationDate;
    this.Questions = Questions;
}

var id = 1;

document.getElementById('examForm').addEventListener('submit', function (event) {
    event.preventDefault();

    var examName = document.getElementById('examName').value; // Exam Name

    var questionType = document.getElementById('questionType').value; // Question type "Will be taken"

    var questionTextarea = document.getElementById('questionBody');// body

    console.log(questionTextarea.value)

    if (questionTextarea) {
        var question = questionTextarea.value;
    } else {
        console.error("Textarea element not found.");
    }
    var Mark = document.getElementById('Mark').value; //Mark value  "Will be taken"

    var options = document.querySelectorAll('.question input[name="option"]'); // Options: multiple values

    var correctOption = document.querySelector('.question input[name="correctOption"]:checked'); // Correct answer

    var isOptionsEmpty = true;

    var optionsHtml = '';

    options.forEach(function (option) {
        if (option.value.trim() !== '') {
            isOptionsEmpty = false;
            optionsHtml += ' ' + option.value;
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

    var correctAns = correctOption.parentElement.querySelector('input[name="option"]').value;  //get correct ans "Will be taken"

    var Q_db_body = questionTextarea.value + " " + optionsHtml; //"Will be taken"


    var q_add = new Question(questionType, Q_db_body, correctAns, Mark);
    global_questions.push(q_add);

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
                    <span class="editable-paragraph" style="border: none; color: green; word-break: break-all;">${correctAns}</span>
                </div>
                <div>
                    <h5 style="display: inline-block;">Mark: </h5>
                    <span class="editable-paragraph" style="border: none; color: green; word-break: break-all;">${Mark}</span>
                </div>
                <a class="btn btn-primary" onclick="modalEditM(this)">Edit</a>
                <a class="btn btn-danger" onclick="modalDeleteM(this)">Delete</a>
            </div>
        `;

    document.getElementById('examQuestions').insertAdjacentHTML('beforeend', questionHtml2);

    // Reset the question body input to empty
    document.getElementById('questionBody').value = '';

    // Reset the question Option input to empty
    options.forEach(function (option) {
        option.value = '';
    });

    // Reset the radio btn input to unchecked
    correctOption.checked = false;


    // Enable submit exam btn
    document.getElementById('submitExam').disabled = false;

    id++;

});
//--------------------------------------------------------------------------------------------------------------//


/* Edit_Question Logic */

const cardModal = new bootstrap.Modal(document.getElementById('cardModal'));
const editQestionBody = document.getElementById('editableTextarea');
const editCorrectAns = document.getElementById('editableInput2');
const editMark = document.getElementById('EditMark2');
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
    Mark = parentDiv.children[3].querySelector('span').innerHTML;


    // Show the data of the question in the card
    editQestionBody.value = question;
    editQuestionChoices.value = optionsHtml;
    editCorrectAns.value = correctAns;
    editMark.value = Mark
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






saveButton.addEventListener('click', function () {
    //save the new values to the initial values
    var newquestion = editQestionBody.value;
    var newoptionsHtml = editableInput1.value;
    var newcorrectAns = editableInput2.value;
    var NewMark = EditMark2.value;
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
                                      <div>
                                        <h5 style="display: inline-block;">Mark: </h5>
                                        <span class="editable-paragraph"  style="border: none; color: green; word-break: break-all;">${NewMark}</span>
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
    //const paragraphQuestionAns = document.getElementById("questionAns");
    const PAnsLable = document.getElementById("PAnsLable");
    const choicesContainer = document.getElementById("choices");
    const deleteQuestionButton = document.getElementById("deleteQuestion");

    // Event listener for question type change
    questionTypeSelect.addEventListener("change", function () {
        if (questionTypeSelect.value === "MCQ") {
            choicesContainer.style.display = "block";
            //paragraphQuestionAns.style.display = "none";
            //    PAnsLable.style.display = "none";
        } else {
            //choicesContainer.style.display = "none";

            //paragraphQuestionAns.style.display = "block";
            //    PAnsLable.style.display = "block";
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

document.getElementById('submitExam').addEventListener('click', function (event) {
    event.preventDefault();

    // Declaration for the from elements
    var examNameElement = document.getElementById('examName');
    var examDurationElement = document.getElementById('examDuration');
    var markElement = document.getElementById('Mark');
    var questionBodyElement = document.getElementById('questionBody');
    var options = document.querySelectorAll('.question input[name="option"]');// Options: multiple values

    console.log("hererereerere");
    global_questions.forEach(function (question) {

        console.log("Body: " + question.Body);

    });

    if (!examNameElement || !examDurationElement || !markElement || !questionBodyElement) {
        console.error("One or more form elements not found.");
        return;
    }



    var courseID = document.getElementById('courseIdContainer').getAttribute('data-course-id');




    $.ajax({
        type: 'GET',
        url: '/Exam/AddExam',
        data: {
            Name: examNameElement.value,
            Duration: examDurationElement.value,
            Questions: JSON.stringify(global_questions),
            CourseId: courseID
        },
        contentType: 'application/json',
        success: function (response) {
            // Handle the success response
            alert('Exam created successfully!');
            window.location.href = '/Exam/Success';
        },
        error: function () {
            alert('Error occurred while creating the exam.');
        }
    });


});