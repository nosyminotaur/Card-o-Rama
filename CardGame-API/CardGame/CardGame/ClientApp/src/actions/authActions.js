export const setLoggedStatus = (status) => {
    return{
        type: "SET_LOGGED_STATUS",
        isLoggedIn: status
    }
}
