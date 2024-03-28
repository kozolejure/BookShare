package org.bookShare;

import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import Class.User;
import org.bson.types.ObjectId;

import java.util.List;

@Path("/user")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class OsebaResource {
    @POST
    public Response createOseba(User oseba) {
        oseba.persist();
        return Response.status(Response.Status.CREATED).entity(oseba).build();
    }

    @GET
    public List<User> listOseb() {
        return User.listAll();
    }

    @GET
    @Path("/{id}")
    public User getOseba(@PathParam("id") String id) {
        return User.findById(new ObjectId(id));
    }

    @PUT
    @Path("/{id}")
    public User updateOseba(@PathParam("id") String id, User oseba) {
        User entity = User.findById(new ObjectId(id));
        if (entity == null) {
            throw new NotFoundException();
        }
        // Posodobi atribute
        entity.setName(oseba.getName());
        entity.setSurname(oseba.getSurname());
        entity.setAddress(oseba.getAddress());
        entity.setCity(oseba.getCity());
        entity.setEmail(oseba.getEmail());
        entity.persistOrUpdate();
        return entity;
    }

    @DELETE
    @Path("/{id}")
    public void deleteOseba(@PathParam("id") String id) {
        User.deleteById(new ObjectId(id));
    }
}
