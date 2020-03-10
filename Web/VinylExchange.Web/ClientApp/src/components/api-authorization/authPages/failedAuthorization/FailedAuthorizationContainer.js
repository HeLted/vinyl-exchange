import React, { Component } from 'react';
import FailedAuthorizationComponent from "./FailedAuthorizationComponent"

class FailedAuthorizationContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return (<FailedAuthorizationComponent/>);
    }
}
 
export default FailedAuthorizationContainer;