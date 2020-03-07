import React, { Component } from 'react';
import SaleInfoComponent from "./SaleInfoComponent";

class SaleInfoContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
  
    render() { 
        return (<SaleInfoComponent data={{sale:this.props.data.sale}}/>);
    }
}
 
export default SaleInfoContainer;