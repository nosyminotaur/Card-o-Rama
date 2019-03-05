const initState = {
    isLoggedIn: false
}

const rootReducer = (state = initState, action) => {
    if(action.type === "SET_LOGGED_STATUS"){
        return {
            ...state,
            isLoggedIn: action.isLoggedIn
        }
    }
    return state;
}

export default rootReducer;