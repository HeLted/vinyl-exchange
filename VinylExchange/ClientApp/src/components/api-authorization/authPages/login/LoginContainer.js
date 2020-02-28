import React, { Component } from 'react';
import LoginComponent from "./LoginComponent"
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import axios from "axios";
import {NotificationContext} from "./../../../../contexts/NotificationContext"
import authService from "./../../../api-authorization/AuthorizeService"
import urlPathSeparator from "./../../../../functions/urlPathSeparator"
import getAntiForgeryAxiosConfig from "./../../../../functions/getAntiForgeryAxiosConfig"

class LoginContainer extends Component {
    constructor() {
        super();
        this.state = {
          usernameInput: "",
          passwordInput: "",
          rememberMeInput:false,
          isLoading:false
        };
      }
    
      static contextType = NotificationContext;
    
      handleOnChange = event => {

        if(event.target.type ==="checkbox"){
          const { value, name } = event.target;
          this.setState(prevstate =>{
            
            return{ rememberMeInput : prevstate.rememberMeInput ? false: true }
          
          });
        }else{
          const { value, name } = event.target;
          this.setState({ [name]: value });
        }
        
      };
    
      handleOnSubmit = event => {
        event.preventDefault();
        event.stopPropagation();
    
        const submitFormObj = {
          username: this.state.usernameInput,
          password: this.state.passwordInput,
          rememberMe:this.state.rememberMeInput
        };
    
        this.setState({isLoading:true})
        axios
          .post(
            Url.authentication +
              Controllers.users.name +
              Controllers.users.actions.login,
            submitFormObj,
            getAntiForgeryAxiosConfig()
          )
          .then(async(response) => {
            if(response.status === 200){

                console.log("hiii");
                let redirectUrl = this.props.location.search.replace(Url.queryStart + Queries.returnUrl + Url.equal,"");
                redirectUrl = urlPathSeparator(redirectUrl);
                const state = { returnUrl:redirectUrl };
                await authService.signIn(state);
                this.context.handleAppNotification("Succesfully logged in", 4);
                this.setState({isLoading:false})
                if(redirectUrl === ""){
                    redirectUrl = "/"
                      this.props.history.push(redirectUrl)
                }else{
                 
                    this.props.history.push(redirectUrl)
                }
            }
            
          })
          .catch(error => {
            this.setState({isLoading:false})
            this.context.handleServerNotification(
              error.response, error.response.status === 401 ?"Invalid credentials!" :
              "There was an error in logging you in!"
            );
          });
      };
    
      render() {
        return (
          <LoginComponent
            data={{
              usernameInput: this.state.usernameInput,
              passwordInput: this.state.passwordInput,
              rememberMeInput: this.state.rememberMeInput,
              isLoading : this.state.isLoading
            }}
            functions={{
              handleOnChange: this.handleOnChange,
              handleOnSubmit: this.handleOnSubmit
            }}
          />
        );
      }
    }
 
export default LoginContainer;