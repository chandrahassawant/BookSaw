# BookSaw

## What it does
 1. A comprehensive online book-selling platform utilizing ASP.NET MVC Framework.
 2. The platform features robust user authentication and authorization using ASP.NET Identity, ensuring secure user access.
 3. It supports three types of users: Admins, Customers, and Sellers, each with distinct roles and responsibilities.
 4. Integrated the Razorpay payment gateway for smooth and secure transactions, enhancing the purchase experience.
 5. Implemented an order tracking feature that allows customers to monitor their orders from purchase to delivery. This project involved full-stack development, including UI design, database management, and thorough testing to ensure a seamless user experience. 


### Key Features:
- **Secure Authentication and Authorization:** Implemented using ASP.NET Identity to ensure that user data is protected, with roles assigned to manage different access levels.
- **Payment Gateway Integration:** Utilizes Razorpay to facilitate secure and efficient payment processing for smooth transactions.
- **Order Tracking:** Allows customers to monitor their orders from purchase to delivery, providing real-time updates and notifications.

## How it works
- **Users:** 
  1. **Admins:** 
     - Manage the overall platform.
     - Oversee user registration and activity.
     - Maintain system security and performance.
     - Handle disputes and ensure compliance with policies.
  2. **Customers:**
     - Browse and search for books using various filters and categories.
     - Add books to the cart and proceed to checkout.
     - Make payments securely using Razorpay.
     - Track order status from purchase to delivery.
     - Review and rate purchased books.
  3. **Sellers:**
     - Register and manage their profiles.
     - List books for sale, including uploading details and pricing.
     - Manage inventory and update book availability.
     - Process orders and arrange for shipping.
     - Monitor sales statistics and performance.

- **Authentication and Authorization:** 
  - Secure login and access management using ASP.NET Identity.
  - Role-based access control to ensure users can only access their respective functionalities.

- **Payment Processing:** 
  - Integrated Razorpay to handle transactions.
  - Ensures secure payment processing with multiple payment options.

- **Order Tracking:** 
  - Customers receive notifications at each stage of the order.
  - Real-time tracking of the order status until delivery.

## Requirements
### Hardware
- Any modern computer with > 8 GB RAM

### Software
- **ASP.NET 8:** For developing the web application.
- **Razorpay API:** For handling payment transactions.
- **Entity Framework:** For database operations.
- **MS SQL:** For data storage and management.

## Architecture
This project utilizes a robust and scalable architecture to manage various functionalities efficiently.

| Component          | Description                    |
|--------------------|--------------------------------|
| **ASP.NET 8**      | Handles the overall structure and flow of the application, ensuring a clean separation of concerns. |
| **Razorpay**       | Manages payment transactions securely and efficiently, providing a seamless checkout experience.    |
| **Entity Framework** | Facilitates database management and operations, making data access simpler and more efficient. |
| **MS SQL**         | Provides reliable data storage and management, ensuring data integrity and availability.            |

## Folder/Directory Structure

- **OnlineBookSellingPlatform**
  - *frontend*
    - Views and static files
  - *backend*
    - *controllers*
      - Controllers and related files
    - *models*
      - Model definitions and files
    - *services*
      - Service logic and related files
  - *database*
    - Migrations and seeders

## Screenshots

![Homepage Screenshot](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/Landing%20Page.png)

![Register Screenshot](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/Register.png)

![Customer HomePage ](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/ProductPage.png)

![Cart ](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/cart.png)

![Payment ](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/Razorpay%20payment.png)

![Payment Done ](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/payment%20sucessfull.png)

![Order Recived To Seller ](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/orderRecivedToSeller.png)

![My Orders Customer Status ](https://github.com/chandrahassawant/BookSaw/blob/main/Screenshots/CustomerOrders.png)




