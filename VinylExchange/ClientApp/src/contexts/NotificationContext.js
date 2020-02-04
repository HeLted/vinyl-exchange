import React, { createContext } from "react";
import uuidv4 from "./../guidGenerator";
export const NotificationContext = createContext();

export default class NotificationContextProvider extends React.Component {
  state = {
    messages: [],
    severity: 0
  };

  handleServerNotification = (notificationObj) => {
    console.log(notificationObj)
    if(notificationObj.status >=400){
      const errorMessages = [];
      const errors = notificationObj.data.errors;

      Object.keys(errors).forEach(function(field) {
        errorMessages.push({messageText:`${field} : ${errors[field].join()}`,id:uuidv4()});
      });
      
      this.setState({
        messages:errorMessages,
        severity:1
      });
    }else{

      const successMessages = [];
      successMessages.push({messageText:notificationObj.data.message,id:uuidv4()})
     
      this.setState({
        messages:successMessages,
        severity:3
      });
    }
    
  };


  handleRemoveNotification = notificationElementId => {
   
    this.setState(prevState => {
       console.log(prevState.messages);
      const updatedMessages = prevState.messages
      .filter(function(element) { return  element.id != notificationElementId; });

      console.log(updatedMessages,notificationElementId)

      return { messages: updatedMessages };
    });
  };


  render() {
    return (
      <NotificationContext.Provider
        value={{
          ...this.state,
          handleServerNotification: this.handleServerNotification,
          handleRemoveNotification : this.handleRemoveNotification
        }}
      >
        {this.props.children}
      </NotificationContext.Provider>
    );
  }
}
