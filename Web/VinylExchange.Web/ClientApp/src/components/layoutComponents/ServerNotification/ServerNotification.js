import React, { Component } from "react";
import "./ServerNotification.css";
import {
  ToastsContainer,
  ToastsStore,
  ToastsContainerPosition
} from "react-toasts";
import { NotificationContext } from "../../../contexts/NotificationContext";

const notificationsIds = [];
const notificationClass = "toast-notification";
const devNotificationClass = notificationClass + " dev-notification";
const saleNotificationClass = notificationClass + " sale-notification";

class ServerNotification extends Component {
  static contextType = NotificationContext;

  componentDidUpdate() {
    let notificationElementTimer = 3000;
    const notificationSeverity = this.context.severity;
    const filteredNotifications = this.context.messages.filter(
      notificationObj => !notificationsIds.includes(notificationObj.id)
    );

    filteredNotifications.forEach(notificationObj => {
      notificationsIds.push(notificationObj.id);
      setTimeout(() => {
        notificationElementTimer += 3000;
        if (notificationSeverity === 1) {
          ToastsStore.error(
            notificationObj.messageText,
            notificationElementTimer,
            notificationClass
          );
        } else if (notificationSeverity === 2) {
          ToastsStore.warning(
            notificationObj.messageText,
            notificationElementTimer,
            notificationClass
          );
        } else if (notificationSeverity === 3) {
          ToastsStore.info(
            notificationObj.messageText,
            notificationElementTimer,
            notificationClass
          );
        } else if (notificationSeverity === 4) {
          ToastsStore.success(
            notificationObj.messageText,
            notificationElementTimer,
            notificationClass
          );
        }else if (notificationSeverity === 6) {
          ToastsStore.success(
            notificationObj.messageText,
            notificationElementTimer,
            saleNotificationClass
          );
        }
        // else if (notificationSeverity === 5) {
        //   ToastsStore.info(
        //     notificationObj.messageText,
        //     notificationElementTimer,
        //     devNotificationClass
        //   );
        // }
      }, 300);
    });

    if (notificationsIds > 20) {
      notificationsIds = [];
    }
  }

  render() {
    return (
      <ToastsContainer
        position={ToastsContainerPosition.TOP_RIGHT}
        store={ToastsStore}
      />
    );
  }
}

export default ServerNotification;
