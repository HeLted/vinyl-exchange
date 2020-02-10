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
    if(notificationObj.status >= 400){
      const errorMessages = [];
     
     if(notificationObj.data.errors != undefined){
      const errors = notificationObj.data.errors;

      Object.keys(errors).forEach(function(field) {
        errorMessages.push({messageText:`${field} : ${errors[field].join()}`,id:uuidv4()});
      });
      
     }else{
       errorMessages.push({messageText:notificationObj.data.message,id:uuidv4()});
     }
      
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

  handleRemoveAllNotifications  = () =>{
    this.setState({ messages: [] });
  }


  render() {
    return (
      <NotificationContext.Provider
        value={{
          ...this.state,
          handleServerNotification: this.handleServerNotification,
          handleRemoveNotification : this.handleRemoveNotification,
          handleRemoveAllNotifications :this. handleRemoveAllNotifications 
        }}
      >
        {this.props.children}
      </NotificationContext.Provider>
    );
  }
}
