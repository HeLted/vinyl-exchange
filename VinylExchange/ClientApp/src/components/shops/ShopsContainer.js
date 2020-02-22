import React, { Component } from 'react';
import ShopsComponent from "./ShopsComponent";

class Shopscontainer extends Component {
    
    constructor() {
        super();
        this.state = {
          searchValue: "",
          filterStyleIds: []
        };
      }
    
      onUpdateSearchValue = newSearchValue => {
        if (this.state.searchValue !== newSearchValue) {
          this.setState({ searchValue: newSearchValue });
        }
      };
    
      onUpdateFilterValue = styleIds => {
        console.log(this.state.filterStyleIds)
        console.log(styleIds);
        
        if (
          JSON.stringify(this.state.filterStyleIds) !== JSON.stringify(styleIds)
        ) {
          this.setState({ filterStyleIds: styleIds });
        }
      };
    
      render() {
        return (
          <ShopsComponent
            functions={{
              onUpdateFilterValue: this.onUpdateFilterValue,
              onUpdateSearchValue: this.onUpdateSearchValue
            }}
            data={{
              searchValue: this.state.searchValue,
              filterStyleIds: this.state.filterStyleIds
            }}
          />
        );
      }
}
 
export default Shopscontainer;