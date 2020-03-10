import React, { Component } from 'react';
import AdminPanelComponent from "./AdminPanelComponent";

class AdminPanelContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
        
    }
    render() { 
        return (<AdminPanelComponent/>);
    }
}
 
export default AdminPanelContainer;