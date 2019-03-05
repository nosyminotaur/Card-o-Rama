export function LoggedIn(dispatch) {
    console.log("Setting localstorage and redux status to logged in!");
    localStorage.setItem("isLoggedIn", true);
    localStorage.setItem("loggedInDateTime", new Date());
    dispatch();
}

export function LoggedOut(dispatch){
    console.log("Setting localstorage and redux status to logged out!");
    localStorage.setItem("isLoggedIn", false);
    localStorage.setItem("loggedInDateTime", null);
    dispatch();
}

export function CheckLoggedIn(){
    console.log("Checking if we are logged in!");
    console.log("TEST:"+localStorage.getItem("isLoggedIn")); 
    if(localStorage.getItem("isLoggedIn") === "true"){  
        if(HasExpired(localStorage.getItem("loggedInDateTime"))){
            console.log("We were logged in but it expired!");
            localStorage.setItem("isLoggedIn", false);
            localStorage.setItem("loggedInDateTime", null);
            return {isLoggedIn: false};
        }
        else{
            console.log("We are already logged in!");
            localStorage.setItem("isLoggedIn", true);
            localStorage.setItem("loggedInDateTime", new Date());
            return {isLoggedIn: true};
        }
    }
    else{
        console.log("isLoggedIn is either false or hasnt been added yet!");
        // isLoggedIn is either false or hasn't yet been added to localstorage
        localStorage.setItem("isLoggedIn", false);
        localStorage.setItem("loggedInDateTime", null);
        return {isLoggedIn: false};
    }
}

function HasExpired(dateTime){
    var currentDateTime = new Date();

    var expirationDate = currentDateTime;
    expirationDate.setMonth(currentDateTime.getMonth + 3);

    return dateTime > expirationDate;
}

