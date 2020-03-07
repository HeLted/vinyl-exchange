import React, { Component } from "react";

class StatusBadge extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  render() {
    const status = this.props.data.status;
    let color = "";
    let badgeText = "";

    if (status === 1) {
      color = "#a2a2a3";
      badgeText = "Open";
    } else if (status === 2) {
      color = "#e87731";
      badgeText = "Shipping Negotiation";
    } else if (status === 3) {
      color = "#e3d132";
      badgeText = "Payment Pending";
    } else if (status === 4) {
      color = "#752fde";
      badgeText = "Paid";
    } else if (status === 5) {
      color = "#db0f5a";
      badgeText = "Sent";
    } else if (status === 6) {
      color = "#20e3cc";
      badgeText = "Finished";
    } else if (status === 7) {
      color = "#1ced20";
      badgeText = "Closed";
    }

    return (
      <a class="badge" style={{backgroundColor:color,padding:"10px",width:"170px"}}>
        {badgeText}
      </a>
    );
  }
}

export default StatusBadge;
