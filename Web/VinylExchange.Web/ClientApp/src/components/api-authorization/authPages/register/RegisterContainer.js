import React, { Component } from "react";
import RegisterComponent from "./RegisterComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import axios from "axios";
import {NotificationContext} from "./../../../../contexts/NotificationContext"
import getAntiForgeryAxiosConfig  from "./../../../../functions/getAntiForgeryAxiosConfig"


class RegisterContainer extends Component {
  constructor() {
    super();
    this.state = {
      usernameInput: "",
      emailInput: "",
      passwordInput: "",
      confirmPasswordInput: "",
      isLoading:false
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();
    event.stopPropagation();

    const submitFormObj = {
      username: this.state.usernameInput,
      email: this.state.emailInput,
      password: this.state.passwordInput,
      confirmPassword: this.state.confirmPasswordInput
    };

    this.setState({isLoading:true})
    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.register,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({isLoading:false})
        if(response.status === 200){
          this.context.handleAppNotification("Succesfully registered", 4);
          this.props.history.push("/Authentication/Login");
        }
        
      })
      .catch(error => {
        this.setState({isLoading:false})
        this.context.handleServerNotification(
          error.response,
          "There was an error in registering account!"
        );
      });
  };

  render() {
    return (
      <RegisterComponent
        data={{
          usernameInput: this.state.usernameInput,
          emailInput: this.state.emailInput,
          passwordInput: this.state.passwordInput,
          confirmPasswordInput: this.state.confirmPasswordInput,
          isLoading: this.state.isLoading
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}


export default RegisterContainer

