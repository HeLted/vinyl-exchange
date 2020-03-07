import React, { Component } from 'react';
import SaleLogComponent from "./SaleLogComponent"

class SaleLogContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { 
            logs:[]
         }
    }
    render() { 
        return (<SaleLogComponent/>);
    }
}
 
export default SaleLogContainer;