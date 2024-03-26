package org.acme;

import io.smallrye.mutiny.Uni;
import jakarta.inject.Inject;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import org.acme.Models.Borrowing;
import org.bson.types.ObjectId;
import org.jboss.resteasy.reactive.RestPath;

import java.time.LocalDate;
import java.util.List;

@Path("/borrowings")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class BorrowingResource {

    @GET
    public Uni<List<Borrowing>> listAll() {
        return Borrowing.listAll();
    }

    @GET
    @Path("/{id}")
    public Uni<Borrowing> getOne(@RestPath String id) {
        return Borrowing.findById(new ObjectId(id));
    }

    @POST
    public Uni<Response> create(Borrowing borrowing) {
        return borrowing.persist()
                .onItem().transform(inserted -> Response.ok(borrowing).status(Response.Status.CREATED).build());
    }

    @PUT
    @Path("/{id}")
    public Uni<Response> update(@RestPath String id, Borrowing borrowingUpdate) {
        return Borrowing.<Borrowing>findById(new ObjectId(id))
                .onItem().ifNotNull().transformToUni(borrowing -> {
                    borrowing.bookId = borrowingUpdate.bookId;
                    borrowing.userId = borrowingUpdate.userId;
                    borrowing.borrowingDate = borrowingUpdate.borrowingDate;
                    borrowing.returnDate = borrowingUpdate.returnDate;
                    return borrowing.update();
                })
                .onItem().transform(updated -> Response.ok(updated).build())
                .onItem().ifNull().continueWith(Response.ok().status(Response.Status.NOT_FOUND)::build);
    }

    @DELETE
    @Path("/{id}")
    public Uni<Response> delete(@RestPath String id) {
        return Borrowing.deleteById(new ObjectId(id))
                .onItem().transform(deleted -> deleted ? Response.ok().status(Response.Status.NO_CONTENT).build() : Response.ok().status(Response.Status.NOT_FOUND).build());
    }


    @Inject
    BorrowingService borrowingService; //

    @GET
    @Path("/soonEnding")
    public Uni<List<Borrowing>> listSoonEndingBorrowings() {
        LocalDate fiveDaysFromNow = LocalDate.now().plusDays(5);
        return Borrowing.<Borrowing>streamAll()
                .filter(borrowing -> borrowing.returnDate != null && borrowing.returnDate.isBefore(fiveDaysFromNow))
                .collect().asList()
                .onItem().invoke(borrowings -> borrowings.forEach(borrowing -> {
                    String message = String.format("Your borrowing of the book with Name %s is ending soon.", borrowing.bookId);
                    borrowingService.sendBorrowingAlert(borrowing.userId.toString(), message);
                }));
    }
}
