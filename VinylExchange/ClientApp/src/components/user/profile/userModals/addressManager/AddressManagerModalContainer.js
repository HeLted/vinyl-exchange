import React, { Component } from "react";
import AddressManagerModalComponent from "./AddressManagerModalComponent";

class AddressManagerModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      modalPageToggler: true
    };
  }

  handleOnToggleModalPage = event => {
    this.setState({
      modalPageToggler: this.state.modalPageToggler ? false : true
    });
  };

  render() {
    return (
      <AddressManagerModalComponent
        data={{ modalPageToggler: this.state.modalPageToggler }}
        functions={{ handleOnToggleModalPage: this.handleOnToggleModalPage }}
      />
    );
  }
}

export default AddressManagerModalContainer;
