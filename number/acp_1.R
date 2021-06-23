require(graphics)

r <- prcomp(~f+c1+c2, 
            data = data.2,
            scale=TRUE)
r
summary(r)
plot(r)
biplot(r)