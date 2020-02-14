import React,{Component} from "react";
import "./ServerNotification.css";
import { NotificationContext } from "../../../contexts/NotificationContext";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes } from "@fortawesome/free-solid-svg-icons";

class ServerNotification extends Component {
  static contextType = NotificationContext;

  constructor() {
    super();
    this.state = {
      messages: [],
      isUpdatedInternally:false
    };
  }

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
        {this.context.messages.map((messageObj, index) => {
          return (
            <div className="d-flex"  key={messageObj.id}>
            <div
              className={"server-notification alert " + alertTypeClass}
              role="alert"
            >
              {messageObj.messageText}

              <FontAwesomeIcon
                onClick={() => this.context.handleRemoveNotification(messageObj.id)}
                icon={faTimes}
              />
            </div>
            </div>
          );
        })}
      </div>
    );
  }
}


export default ServerNotification;