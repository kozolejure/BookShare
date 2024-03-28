package Class;
import io.quarkus.mongodb.panache.PanacheMongoEntity;
import io.quarkus.mongodb.panache.common.MongoEntity;

import java.util.List;
import java.util.List;

@MongoEntity(collection="User")
public class User extends PanacheMongoEntity {


    public String name;
    public String surname;
    public String address;
    public String city;
    public String email;

    // Polni konstruktor
    public User(String name, String surname, String address, String city, String email, List<Integer> booksToOffer) {
        this.name = name;
        this.surname = surname;
        this.address = address;
        this.city = city;
        this.email = email;

    }


    public User() {

    }

    // Geterji

    public String getName() {
        return name;
    }

    public String getSurname() {
        return surname;
    }

    public String getAddress() {
        return address;
    }

    public String getCity() {
        return city;
    }

    public String getEmail() {
        return email;
    }



    // Seterji

    public void setName(String name) {
        this.name = name;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public void setEmail(String email) {
        this.email = email;
    }


}
