import React from "react";
import "./ServerNotification.css";
import { NotificationContext } from "./../contexts/NotificationContext";

export default class ServerNotification extends React.Component {
  static contextType = NotificationContext;

  render() {
    let alertTypeClass = "alert-secondary";

    const notificationSeverity = this.context.severity;

    if (notificationSeverity === 3) {
      alertTypeClass = "alert-success";
    } else if (notificationSeverity === 2) {
      alertTypeClass = "alert-primary";
    } else if (notificationSeverity === 1) {
      alertTypeClass = "alert-danger";
    }

    return (
      <div className="server-notification-wrapper">
        {this.context.messages.map((messageText, index) => {
          return (
            <div
              className={"server-notification alert " + alertTypeClass}
              key={index}
              role="alert"
            >
              {messageText}
            </div>
          );
        })}
      </div>
    );
  }
}
