import React,{Component} from "react";
import SearchShopsComponent from "./SearchShopsComponent";

 class SearchShopsContainer extends Component {
  constructor() {
    super();

    this.state = {
      searchValue: "",
      isTyping: false
    };

    this.timer = null;
  }

  handleOnChange = event => {
    this.setState({ searchValue: event.target.value, isTyping: true });
  };

  componentDidUpdate() {
    if (this.state.isTyping) {
      clearTimeout(this.timer);
      this.timer = setTimeout(() => {
        this.setState(prevState => {
          return { ...prevState, isTyping: false };
        });
        this.props.onUpdateSearchValue(this.state.searchValue);
      }, 1000);
    }
  }

  render() {
    return (
      <SearchShopsComponent
        inputOnChangeFunction={this.handleOnChange}
        searchInputValue={this.state.searchValue}
        isTyping={this.state.isTyping}
      />
    );
  }
}

export default SearchShopsContainer;