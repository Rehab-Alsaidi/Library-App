# Library Web App - Book Recommendation System

## Overview
This project is a **Library Web Application** that provides **personalized book recommendations** based on user preferences. The system utilizes a **recommendation algorithm** trained on a large dataset of book interactions and user preferences. The application is built using **ASP.NET** for the backend and includes data processing, model training, and integration.

## Features
- **User Authentication**: Users can register, log in, and manage their profiles using **Microsoft Identity**.
- **Personalized Recommendations**: The system suggests books based on user history and preferences.
- **Search & Browse**: Users can search for books by title, author, or genre.
- **Data-Driven Insights**: The recommendation model is trained on a dataset of **1 million samples**.
- **Cleaned & Preprocessed Data**: Data was carefully cleaned before training to ensure high-quality recommendations.
- **User Feedback Mechanism**: Users can rate books to improve recommendations over time.
- **Admin Dashboard**: Manage books, users, and system configurations.

## Data Collection & Processing
- **Dataset**: The dataset consists of **1 million** book interactions.
- **Data Cleaning**:
  - Removed duplicate entries
  - Handled missing values
  - Standardized book titles, author names, and genres
  - Filtered out low-quality or irrelevant records
- **Preprocessing**:
  - Encoded categorical variables
  - Normalized numerical features
  - Split the dataset into training, validation, and test sets

## Recommendation Model
The system utilizes a **collaborative filtering-based recommendation algorithm**, trained on the cleaned dataset. The model considers:
- **User preferences** (past ratings and interactions)
- **Book similarities** (based on genres, authors, and keywords)
- **Implicit and explicit feedback**

## Tech Stack
- **Frontend**: HTML, CSS, JavaScript
- **Backend**: ASP.NET
- **Authentication**: Microsoft Identity
- **Database**: SQL Server
- **Machine Learning**: Python (for data preprocessing and model training)
- **Hosting**: IIS / Cloud deployment options

## Code Upload
The **ASP.NET source code** will be uploaded as part of this repository for reference and further development.

## Future Enhancements
- **Real-time recommendations** based on browsing activity
- **Integration with external book APIs** for additional data
- **Mobile app version** for seamless user experience

