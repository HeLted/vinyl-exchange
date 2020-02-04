import React, { createContext } from "react";

export const NotificationContext = createContext();

export default class NotificationContextProvider extends React.Component {
  state = {
    messages: [],
    severity: 0
  };

  handleServerNotification = (notificationObj) => {

    if(notificationObj.status >=400){
      const errorMessages = [];
      const errors = notificationObj.data.errors;

      Object.keys(errors).forEach(function(field) {
        errorMessages.push(`${field} : ${errors[field].join()}`);
      });
      
      console.log(errorMessages)
      this.setState({
        messages:errorMessages,
        severity:1
      });
    }else{

      const successMessages = [];
      successMessages.push(notificationObj.data.message)
      console.log(notificationObj)
      this.setState({
        messages:successMessages,
        severity:3
      });
    }
    
  };

  render() {
    return (
      <NotificationContext.Provider
        value={{
          ...this.state,
          handleServerNotification: this.handleServerNotification
        }}
      >
        {this.props.children}
      </NotificationContext.Provider>
    );
  }
}
