import React, { createContext } from "react";

export const NotificationContext = createContext();

export default class NotificationContextProvider extends React.Component {
  state = {
    errors: [],
    statusCode: 0
  };

  handleServerNotification = error => {

    this.setState({
      errors:error.data.errors,
      statusCode:error.status
    });
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
