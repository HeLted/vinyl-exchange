import React, { Component, ReactChild } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import Player from "./common/Player";
import { Helmet } from "react-helmet";
import ServerNotification from "./ServerNotification";

export class Layout extends Component {
  static displayName = Layout.name;

  constructor() {
    super();
    this.state = {
      currentNotificationMessage: "",
      currentNotificationSeverity: 0
    };
  }

  handleServerNotification = (notificationMessage ,severty) => {
    console.log("in handleservernotification")
    this.setState({
      currentNotificationMessage: notificationMessage,
      currentNotificationSeverity: severty
    });
  };

  renderChildren() {
    return React.Children.map(this.props.children, child => {
      if (child.type === ReactChild) {
        return React.cloneElement(child, {
          global: {
            state: this.state,
            functions: {
              handleServerNotification: this.handleServerNotification
            }
          }
        });
      } else {
        return child;
      }
    });
  }

  render() {
    return (
      <div>
        <NavMenu />
        <ServerNotification
          message={this.state.currentNotificationMessage}
          severity={this.state.severity}
        />
        <Container globalstate={this} fluid={true}>
          {this.renderChildren()}
          <Player />
        </Container>
      </div>
    );
  }
}
