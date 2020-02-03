import React, { Component ,ReactChild } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import Player from "./common/Player";
import { Helmet } from "react-helmet";
import ServerNotification from "./ServerNotification"

export class Layout extends Component {
  static displayName = Layout.name;

  constructor() {
    super();
    this.state = {
      currentNotificationMessage: "",
      currentNotificationSeverity: 0
    };
   
  }

  handleNotification = (notificationMessageAndSeverty) => {
   console.log("in")
    //format: ErrorHappened<->2
    let splitted = notificationMessageAndSeverty.split("<->")
    this.setState({
      
      currentNotificationMessage: splitted[0],
      currentNotificationSeverity: parseInt(splitted[0])
    });
  };

  renderChildren(){
   
    return React.Children.map(this.props.children,child=>{
      if(child.type === ReactChild ){
        return React.cloneElement(child,{
          globalState:this
        })
      }else{
        return child
      }
    })
  }

 

  render() {
    

    return (
      <div>
        <NavMenu />
        <ServerNotification message={this.state.currentNotificationMessage} severity={this.state.severity}/>
        <Container globalstate={this} fluid={true}>
          {this.renderChildren()}
          <Player />
        </Container>
      </div>
    );
  }
}
