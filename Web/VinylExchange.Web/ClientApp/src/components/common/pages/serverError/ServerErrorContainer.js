import React, { Component } from 'react';
import ServerErrorComponent from "./ServerErrorComponent"

class ServerErrorContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return ( <ServerErrorComponent/> );
    }
}
 
export default ServerErrorContainer;