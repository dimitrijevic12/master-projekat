import React, { Component } from "react";
import OwnerCourses from "../components/Courses/OwnerCourses";
import Layout from "../layouts/Layout";

class OwnerCoursesPage extends Component {
  render() {
    return (
      <Layout>
        <OwnerCourses />
      </Layout>
    );
  }
}

export default OwnerCoursesPage;
