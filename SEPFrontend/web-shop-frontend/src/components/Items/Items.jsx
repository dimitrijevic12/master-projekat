import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getItems } from "../../actions/actionsItems";

class Items extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getItems();
  }
  render() {
    if (this.props.items === undefined) {
      return null;
    }

    const items = this.props.items;
    const ItemsList = () => {
      debugger;
      console.log(items);
      return "123456";
    };

    // const Products = () => {
    //   return items.map((post, i) =>
    //     this.props.collectionImages[i].contentType === "image/jpeg" ? (
    //       <Photo
    //         src={
    //           "data:image/jpg;base64," +
    //           this.props.collectionImages[i].fileContents
    //         }
    //         onClick={() => this.displayModalPost(post)}
    //       />
    //     ) : (
    //       <video
    //         controls
    //         onClick={() => this.displayModalPost(post)}
    //         style={{ width: 367, height: 370 }}
    //         className="mb-3"
    //       >
    //         <source
    //           src={
    //             "data:video/mp4;base64," +
    //             this.props.collectionImages[i].fileContents
    //           }
    //           type="video/mp4"
    //         ></source>
    //       </video>
    //     )
    //   );
    // };

    return (
      <div>
        {/* {this.state.showPostModal ? (
          <PostModal
            show={this.state.showPostModal}
            postId={this.state.postId}
            personPhoto="/images/download.jfif"
            person={this.state.username}
            onShowChange={() => this.displayModalPost()}
          />
        ) : null} */}
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Items</GridControlBarItem>
          </GridControlBar>
          <ItemsList />
        </Grid>
      </div>
    );
  }

  //   displayModalPost = (post) => {
  //     if (post != undefined) {
  //       this.setState({
  //         postId: post.id,
  //         username: post.registeredUser.username,
  //       });
  //     }
  //     this.setState({
  //       showPostModal: !this.state.showPostModal,
  //     });
  //   };

  //   getAllImages = async (items) => {
  //     const paths = [];
  //     for (var i = 0; i < items.length; i++) {
  //       if (items[i].contentPath === undefined) {
  //         paths.push(items[i].contentPaths[0]);
  //       } else {
  //         paths.push(items[i].contentPath);
  //       }
  //     }
  //     await this.props.getAllImagesForCollection(paths);
  //   };
}

const mapStateToProps = (state) => ({
  items: state.items,
  //collectionImages: state.collectionImages,
});

export default compose(
  connect(mapStateToProps, {
    getItems,
  })
)(Items);
