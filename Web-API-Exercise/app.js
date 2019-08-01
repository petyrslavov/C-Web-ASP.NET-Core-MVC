const appUrl = 'https://localhost:44338/api/';
let currentUsername = null;


// function chooseUsername(){
//     let username = $('#username').val();

//     if (username.length === 0) {
//         alert('You cannot choose an empty username!')
//         return;
//     }

//     currentUsername = username;
//     $('#username-choice').text(currentUsername);
//     $('#choose-data').hide();
//     $('#reset-data').show();
// }

// function resetUsername(){
//     currentUsername = null;
//     $('#choose-data').show();
//     $('#reset-data').hide();
// }

// $('#reset-data').hide();
loadMessages();
