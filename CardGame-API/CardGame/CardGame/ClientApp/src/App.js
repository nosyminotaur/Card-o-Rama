import React, { Component } from 'react';
import './App.css';
import GoogleLogin from 'react-google-login';
import { GoogleLogout } from 'react-google-login';

class App extends Component {

  googleResponse = (response) => {

    const options = {
      method: 'POST',
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify({idToken: response.tokenId,
                           googleEmail: response.profileObj.email,
                           username: response.profileObj.name})
    };

    console.log("RESPONSE:");
    console.log(response);


    fetch("https://localhost:5001/api/g-login", options)
      .then(response => response.text())
      .then(data => console.log(data));
  };

  render() {

    return (
      <div>

        <GoogleLogin
          clientId="769546831930-e6tpvqoeck9kq3dgviq1tbnoteg6lecl.apps.googleusercontent.com"
          buttonText="Login with google"
          onSuccess={this.googleResponse}
          onFailure={this.googleResponse}
        />

        <GoogleLogout
          clientId="769546831930-e6tpvqoeck9kq3dgviq1tbnoteg6lecl.apps.googleusercontent.com"
          buttonText="Logout"
        >
        </GoogleLogout>

      </div>
    );
  }
}

export default App;
