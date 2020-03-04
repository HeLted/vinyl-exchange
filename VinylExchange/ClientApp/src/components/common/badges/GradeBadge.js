import React, { Component } from "react";

class GradeBadge extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  render() {
    const grade = this.props.data.grade;
    let color = "";
    let badgeText = "";

    if (grade === 0) {
      color = "#a2a2a3";
      badgeText = "Not Selected";
    } else if (grade === 1) {
      color = "#e87731";
      badgeText = "Poor";
    } else if (grade === 2) {
      color = "#e3d132";
      badgeText = "Fair";
    } else if (grade === 3) {
      color = "#752fde";
      badgeText = "Good";
    } else if (grade === 4) {
      color = "#db0f5a";
      badgeText = "Very Good";
    } else if (grade === 5) {
      color = "#20e3cc";
      badgeText = "Near Mint";
    } else if (grade === 6) {
      color = "#1ced20";
      badgeText = "Mint";
    }

    return (
      <span className="badge badge-secondary property-text-nm" style={{ backgroundColor: color ,padding:"10px",whiteSpace:"normal",width:"70px"}}>
        {badgeText}
      </span>
    );
  }
}

export default GradeBadge;
