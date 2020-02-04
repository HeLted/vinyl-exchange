import React from "react";
import "./ServerNotification.css"
import { NotificationContext } from "./../contexts/NotificationContext";

export default class ServerNotification extends React.Component {
  static contextType = NotificationContext;

  render() {
    let alertTypeClass = "alert-success";

    let errors = this.context.errors;
    let errorDisplayText = [];

    Object.keys(errors).forEach(function(field) {
      errorDisplayText.push(`${field} : ${errors[field].join()}`);
    });

    if (1 === 3) {
      alertTypeClass = "alert-success";
    } else if (1 === 2) {
      alertTypeClass = "alert-primary";
    } else if (1 === 1) {
      alertTypeClass = "alert-danger";
    }

    return (
      <div className="server-notification-wrapper">
        {errorDisplayText.map((errorText,index) => {
        return <div className={"server-notification alert " + alertTypeClass} key={index} role="alert">{errorText}</div>;
        })}
      </div>
    );
  }
}
